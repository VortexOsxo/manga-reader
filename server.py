from flask import Flask

from blueprints.mangas import manga_bp
from blueprints.login import login_bp
from blueprints.signup import signup_bp
from blueprints.favorites import favorite_bp
from blueprints.histories import histoy_bp

from services.database.database_creator import DatabaseCreator

from flask_cors import CORS

app = Flask(__name__)
CORS(app)

app.register_blueprint(manga_bp, url_prefix='/mangas')
app.register_blueprint(login_bp, url_prefix='/login')
app.register_blueprint(signup_bp, url_prefix='/signup')
app.register_blueprint(favorite_bp, url_prefix='/favorites')
app.register_blueprint(histoy_bp, url_prefix='/histories')

if __name__ == '__main__':
    DatabaseCreator.verify_database_is_created()
    app.run(debug=False, host='0.0.0.0', port=5000)
