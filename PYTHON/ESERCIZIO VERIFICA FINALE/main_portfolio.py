'''
RICCARDO CASAROTTI
BAD 
2026
'''


from portfolio import Portfolio
from stock import Stock
from termcolor import colored

# VINCOLO: Utilizzare il main()
def main():
    # VINCOLO: Rispettare la convenzione di naming snake_case (es: mio_portafoglio invece di MioPortafoglio)
    mio_portafoglio = Portfolio("Investment For Future", "Gerry Scotti")

    print(colored("--- FASE DI ACQUISTO ---", "yellow"))
    mio_portafoglio.buy_stock("ORCL", 10, 150.0) 
    mio_portafoglio.buy_stock("LNRD", 5, 300.0)
    mio_portafoglio.buy_stock("MICROSOFT AZURE", 2, 2500.0) # Attiva il blocco except: lo stock ha troppi caratteri

    print(colored("\n--- FASE DI VENDITA ---", "yellow"))
    mio_portafoglio.sell_stock("LNRD", 2, 300.0)
    mio_portafoglio.sell_stock("LNRD", 1, 5000.0) # --SE PONGO A COMMENTO QUESTA RIGA CAMBIA OUTPUT SALDO FINALE NEL RIEPILOGO ECONOMICO 
    mio_portafoglio.sell_stock("GOOGLE",2,100.0)


    print(colored("\n--- TEST ERRORI ---", "yellow"))
    mio_portafoglio.sell_stock("GMNI", 10, 200.0)
    mio_portafoglio.sell_stock("ORCL", 50, 180.0)
    mio_portafoglio.view_single_stock("HWEI")

    print(colored("\n--- VISUALIZZAZIONE SINGOLA AZIONE ---", "yellow"))
    mio_portafoglio.view_single_stock("ORCL")

    mio_portafoglio.view_portfolio()
    mio_portafoglio.show_summary()

    print(colored("--- METODO E ATTRIBUTO DI CLASSE ---", "yellow"))
    # Richiamo del metodo di classe testato nel main
    totale_azioni_create = Stock.get_stocks_created_count()
    print(colored(f"Totale differenti oggetti Stock inizializzati: {totale_azioni_create}", "magenta"))

if __name__ == "__main__":
    main()