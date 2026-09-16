import azure.functions as func
import logging
import json

# Inizializziamo l'applicazione delle funzioni per il worker
app = func.FunctionApp()

# 1. Trigger: si attiva quando arriva un messaggio in coda
@app.queue_trigger(arg_name="msg", queue_name="queue4apptest", connection="key4apptest")
# 2. 👇 OTTIMIZZAZIONE SOVRASCRIZIONE: Usando un nome fisso, Azure sovrascriverà questo esatto file ad ogni avvio.
@app.blob_output(arg_name="dati-output", path="dati-output/ultimo-record.json", connection="key4apptest")
# 3. Output: scrive il messaggio finale nella coda di risposta
@app.queue_output(arg_name="replymsg", queue_name="risposte-coda", connection="key4apptest")
def queue_worker(msg: func.QueueMessage, outputblob: func.Out[str], replymsg: func.Out[str]) -> None:
    logging.info('Il Worker Python ha rilevato un nuovo messaggio dalla coda di input.')

    # Leggiamo e decodifichiamo il testo del messaggio
    corpo_messaggio = msg.get_body().decode('utf-8')
    logging.info(f"Contenuto ricevuto dalla coda: {corpo_messaggio}")

    # 👇 Scrive il JSON sovrascrivendo il Blob Storage
    outputblob.set(corpo_messaggio)
    logging.info("Dati scritti (e sovrascritti) con successo nel Blob Storage!")

    # Estraiamo i dati dell'utente per inserirli nella risposta
    try:
        dati_utente = json.loads(corpo_messaggio)
        nome = dati_utente.get("nome", "N/D")
        cognome = dati_utente.get("cognome", "N/D")
        ruolo = dati_utente.get("ruolo", "N/D")
    except Exception:
        nome, cognome, ruolo = "Errore", "Lettura", "Dati"

    # Prepariamo il payload JSON di risposta
    payload_risposta = {
        "status": "successo",
        "messaggio": "record letto e generato correttamente sul database",
        "dettaglio_utente": {
            "nome": nome,
            "cognome": cognome,
            "ruolo": ruolo
        }
    }

    # Inviamo il JSON di conferma alla coda di risposta
    replymsg.set(json.dumps(payload_risposta))
    logging.info("Messaggio di conferma registrato nella coda di risposta!")