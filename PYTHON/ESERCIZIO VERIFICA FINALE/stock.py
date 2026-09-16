'''
RICCARDO CASAROTTI
BAD
2026
'''

class Stock:
    # VINCOLO: Introdurre almeno un attributo di classe
    total_stocks_created = 0

    def __init__(self, ticker, quantity=0):
        self.ticker = self.validate_ticker(ticker)
        self.quantity = quantity
        Stock.total_stocks_created += 1

    # VINCOLO: Introdurre almeno un metodo statico
    @staticmethod
    def validate_ticker(ticker):
        # VINCOLO: Normalizzare il nome del ticker dell’azione con strip() e upper()
        clean_ticker = ticker.strip().upper()
        
        # VINCOLO: Per nome dell’azione usare ticker lungo 4 caratteri, controllare la correttezza
        if len(clean_ticker) != 4:
            raise ValueError(f"Il ticker '{clean_ticker}' deve essere lungo esattamente 4 caratteri.")
            
        return clean_ticker

    # VINCOLO: Introdurre almeno un metodo di classe
    @classmethod
    def get_stocks_created_count(cls):
        return cls.total_stocks_created

    def add_quantity(self, amount):
        if amount > 0:
            self.quantity += amount

    def remove_quantity(self, amount):
        if 0 < amount <= self.quantity:
            self.quantity -= amount
            return True
        return False

    def __str__(self):
        return f"Azione: {self.ticker} | Quantità posseduta: {self.quantity}"