from model.user import User
import uuid
from datetime import datetime, timedelta
import time
from threading import Thread
from config import SESSIONS_DURATIONS

class SessionsService:
    def __init__(self):
        self.users: map[str, str] = {}
        self.sessions: map[str, list[User, datetime]] = {}

        self.session_timeout = timedelta(minutes=SESSIONS_DURATIONS)

    def get_user(self, session_id: str) -> User:
        session_uuid =  uuid.UUID(session_id)
        if session_uuid not in self.sessions: return None

        self.sessions[session_uuid][1] = datetime.now()

        return self.sessions[session_uuid][0]
    
    def add_user(self, user: User) -> str:
        if user.id in self.users:
            return self.users[user.id]

        session_id = uuid.uuid4()

        self.users[user.id] = session_id
        self.sessions[session_id] = [user, datetime.now()]
        
        return session_id
    
    def _background_thread(self):
        time.sleep(15)
        self._remove_expired_session()
        self._background_thread()
    
    def _remove_expired_session(self):
        sessions_id_to_remove = []

        current_time = datetime.now()

        for session_id, (user, session_time) in self.sessions.items():
            if (current_time > session_time + self.session_timeout):
                sessions_id_to_remove.append(session_id)

        for session_id in sessions_id_to_remove:
            user, _ = self.sessions[session_id]
            del self.sessions[session_id]
            del self.users[user.id]
            
sessions_service = SessionsService()
Thread(target=sessions_service._background_thread).start()
