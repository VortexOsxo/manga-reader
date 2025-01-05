from flask import Blueprint, Response
from services.authentication.encription_service import EncriptionService
from services.database.users_database_service import UsersDatabaseService
from middlewares.authentication_details import authentication_details

signup_bp = Blueprint('signup', __name__)

@signup_bp.route('/', methods=['POST'])
@authentication_details
def signup(username: str, password: str):
    usersDbService = UsersDatabaseService()

    user = usersDbService.get_user_by_username(username)
    if (user is not None): Response('Username already exist', mimetype='text/plain', status=400)

    hashed_password = EncriptionService.hash_password(password)
    usersDbService.add_user(username, hashed_password)
    return Response('Account created', mimetype='text/plain', status=201)
