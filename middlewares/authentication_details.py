from functools import wraps
from flask import request

def authentication_details(func):
    @wraps(func)
    def decorated_function(*args, **kwargs):
        username = request.json.get('username')
        password = request.json.get('password')

        return func(*args, **kwargs, username=username, password=password)
    
    return decorated_function