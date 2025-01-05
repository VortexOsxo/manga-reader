from functools import wraps
from flask import Response, request

from services.sessions_service import sessions_service

def user_authentication_middleware(func):
    @wraps(func)
    def decorated_function(*args, **kwargs):
        if 'User-Session-Id' not in request.headers:
            return Response('No session id provided', mimetype='text/plain', status=401)
        
        session_id = request.headers.get('User-Session-Id')
        user = sessions_service.get_user(session_id)
        
        if user is None:
            return Response('Invalid session id', mimetype='text/plain', status=403)

        return func(*args, **kwargs, user=user)
    
    return decorated_function