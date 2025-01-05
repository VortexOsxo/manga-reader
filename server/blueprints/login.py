from flask import Blueprint, jsonify, Response
from services.authentication.encription_service import EncriptionService
from services.database.users_database_service import UsersDatabaseService
from services.sessions_service import sessions_service
from middlewares.authentication_details import authentication_details

login_bp = Blueprint('login', __name__)

@login_bp.route('/', methods=['POST'])
@authentication_details
def login(username: str, password: str):
    usersDbService = UsersDatabaseService()
    user = usersDbService.get_user_by_username(username)
    if user is None:
        return Response('Account not found', mimetype='text/plain', status=404)

    success = EncriptionService.compare_password(password, user.hashed_password)
    if not success:
        return Response('Wrong password', mimetype='text/plain', status=401)

    session_id = sessions_service.add_user(user)

    if success: return jsonify(message="Login Successful", session_id=session_id), 200

