import bcrypt

class EncriptionService:
    @staticmethod
    def hash_password(password: str) -> bytes:
        return bcrypt.hashpw(password.encode('utf-8') , bcrypt.gensalt()) 

    @staticmethod
    def compare_password(password: str, hash: bytes) -> bool:
        userBytes = password.encode('utf-8')
        return bcrypt.checkpw(userBytes, hash)

