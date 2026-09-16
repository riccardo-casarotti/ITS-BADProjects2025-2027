import json
import time
from azure.storage.queue import QueueClient

# --- CONFIGURAZIONE ---
# Inserisci qui la TUA connection string dello storage account di Azure (o Azurite)
CONNECTION_STRING = "your connection string here"

QUEUE_NAME = "your queue name here"
SUCCESS_QUEUE_NAME = "your success queue name here"

def start_worker():
    print("Inizializzazione del worker in corso...")
    
    # Creiamo i client per leggere dalla coda principale e scrivere in quella di successo
    queue_client = QueueClient.from_connection_string(conn_str=CONNECTION_STRING, queue_name=QUEUE_NAME)
    success_queue = QueueClient.from_connection_string(conn_str=CONNECTION_STRING, queue_name=SUCCESS_QUEUE_NAME)

    # Crea la coda di successo se non esiste già
    try:
        success_queue.create_queue()
    except Exception:
        pass # La coda esiste già

    print(f"🎧 Worker in ascolto sulla coda '{QUEUE_NAME}'... (Premi Ctrl+C per uscire)")

    try:
        # Ciclo infinito per tenere lo script in vita
        while True:
            # Chiediamo ad Azure se ci sono messaggi (ne prendiamo uno alla volta)
            messages = queue_client.receive_messages(max_messages=1, visibility_timeout=30)
            
            message_processed = False
            
            for msg in messages:
                message_processed = True
                try:
                    # 1. Decodifica e lettura del JSON
                    # azure-storage-queue restituisce il contenuto come stringa
                    data = json.loads(msg.content)
                    
                    nome = data.get("nome", "Sconosciuto")
                    cognome = data.get("cognome", "Sconosciuto")
                    
                    print(f"\n🔄 Trovato messaggio! Elaborazione per: {nome} {cognome}...")
                    
                    # --- QUI AVVIENE LA TUA LOGICA DI DATABASE ---
                    # es: db.salva_utente(data)
                    
                    # 2. Scrittura del messaggio nella coda di successo
                    messaggio_conferma = f"Utente {nome} {cognome}: aggiunto correttamente al database e consumato correttamente"
                    success_queue.send_message(messaggio_conferma)
                    print(f"✅ Messaggio inviato a '{SUCCESS_QUEUE_NAME}'.")

                    # 3. Elimina il messaggio dalla coda principale (conferma di avvenuta elaborazione)
                    queue_client.delete_message(msg.id, msg.pop_receipt)
                    print("🗑️ Messaggio originale eliminato dalla coda.")

                except json.JSONDecodeError:
                    print(f"❌ Errore: Il messaggio (ID: {msg.id}) non è un JSON valido.")
                except Exception as e:
                    print(f"❌ Errore imprevisto durante l'elaborazione: {str(e)}")
            
            # Se non ci sono messaggi, aspetta 5 secondi prima di controllare di nuovo 
            # (evita di spammare richieste ad Azure consumando risorse inutilmente)
            if not message_processed:
                time.sleep(5)

    except KeyboardInterrupt:
        print("\n🛑 Worker fermato manualmente dall'utente.")

if __name__ == '__main__':
    start_worker()