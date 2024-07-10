from model.user import User
import uuid

class SessionsService:
    def __init__(self):
        self.users: map[str, str] = {}
        self.sessions: map[str, User] = {}

    def get_user(self, session_id: str) -> User:
        session_uuid =  uuid.UUID(session_id)
        if session_uuid not in self.sessions: return None
        return self.sessions[session_uuid]
    
    def add_user(self, user: User) -> str:
        if user.id in self.users:
            return self.users[user.id]

        session_id = uuid.uuid4()

        self.users[user.id] = session_id
        self.sessions[session_id] = user
        
        return session_id
    
sessions_service = SessionsService()
