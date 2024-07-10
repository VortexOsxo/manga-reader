import sqlite3

class DatabaseService:
    def __init__(self, db_name='manga.db'):
        self.db_name = db_name

    def __enter__(self):
        self.connection = sqlite3.connect(self.db_name)
        self.cursor = self.connection.cursor()
        return self

    def __exit__(self, exc_type, exc_value, traceback):
        self.connection.commit() if exc_type is None else self.connection.rollback()
        self.connection.close()

    def execute_query(self, query: str, values: tuple = ()) -> None:
        self.cursor.execute(query, values)

    def select_query(self, query: str, values: tuple = ()) -> list:
        self.cursor.execute(query, values)
        return self.cursor.fetchall()
    
    def select_one_query(self, query: str, values: tuple = ()) -> tuple:
        self.cursor.execute(query, values)
        return self.cursor.fetchone()
