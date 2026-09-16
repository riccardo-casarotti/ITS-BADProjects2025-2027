'''
RICCARDO CASAROTTI
BAD
2026
'''

from stock import Stock
# VINCOLO: Utilizzare termcolor per l’output e la visualizzazione degli errori
from termcolor import colored

class Portfolio:
    def __init__(self, name, owner):
        self.name = name
        self.owner = owner
        self.stocks = {}
        self.total_spent = 0.0
        self.total_earned = 0.0

    def buy_stock(self, ticker, quantity, price):
        # VINCOLO: Utilizzare try / except / else / finally
        try:
            clean_ticker = Stock.validate_ticker(ticker)
        except ValueError :
            print(colored(f"ERRORE ACQUISTO - Nome ticker : {ticker}", "red"))
        else:
            if clean_ticker not in self.stocks:
                self.stocks[clean_ticker] = Stock(clean_ticker)
            self.stocks[clean_ticker].add_quantity(quantity)
            self.total_spent += (quantity * price)
            print(colored(f"OK - Acquisto registrato: {quantity} {clean_ticker}.", "green"))
        finally:
            # Istruzione sempre eseguita al termine del blocco try
            print(colored("-"*20, "white"))

    def sell_stock(self, ticker, quantity, price):
        try:
            clean_ticker = Stock.validate_ticker(ticker)
        except ValueError:
            print(colored(f"ERRORE VENDITA - Ticker non presente!!", "red"))
            return

        if clean_ticker not in self.stocks:
            print(colored(f"!!!! ERRORE - Tentativo di vendita fallito: l'azione {clean_ticker} non è presente nel portafoglio. !!!!", "red"))
            return

        if self.stocks[clean_ticker].remove_quantity(quantity):
            self.total_earned += (quantity * price)
            print(colored(f"OK - Vendita registrata: {quantity} {clean_ticker}.", "green"))
        else:
            print(colored(f"!!!! ERRORE - Tentativo di vendita fallito: la quantità richiesta ({quantity}) è maggiore di quella disponibile per {clean_ticker}. !!!!", "red"))

    def view_single_stock(self, ticker):
        try:
            clean_ticker = Stock.validate_ticker(ticker)
        except ValueError:
            print(colored(f"----ERRORE LETTURA ----", "red"))
            return
            
        if clean_ticker in self.stocks:
            print(colored(str(self.stocks[clean_ticker]), "cyan"))
        else:
            print(colored(f"!!!! ERRORE - Impossibile visualizzare: l'azione {clean_ticker} non è presente nel portafoglio.!!!!", "red"))

    def view_portfolio(self):
        print(colored(f"\n=== Portafoglio: {self.name} | Titolare: {self.owner} ===", "magenta", ))
        if not self.stocks:
            print(colored("Il portafoglio attualmente è vuoto.", "yellow"))
        else:
            for stock in self.stocks.values():
                print(colored(f" - {stock}", "cyan"))
        print(colored("---------------------------", "blue", ))

    def show_summary(self):
        final_balance = self.total_earned - self.total_spent
        print(colored(f"\n--- Riepilogo Economico | Proprietario:  {self.owner}---", "blue", ))
        
        # VINCOLO: Visualizzazione delle cifre con 3 cifre decimali
        print(colored(f"Totale acquisti: €{self.total_spent:.3f}", "blue"))
        print(colored(f"Totale vendite:  €{self.total_earned:.3f}", "blue"))
        if final_balance > 0:
            print(colored(f"Saldo finale:    €{final_balance:.3f}", "green"))
        else:
            print(colored(f"Saldo finale:    €{final_balance:.3f}", "red"))
        print(colored("---------------------------\n", "blue", ))