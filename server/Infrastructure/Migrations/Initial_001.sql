CREATE TABLE users(
    id UUID PRIMARY KEY,
    nickname VARCHAR(128) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE games(
    id UUID PRIMARY KEY NOT NULL,
    winner_id INTEGER,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL
);

CREATE TABLE users_games(
    id SERIAL PRIMARY KEY,
    user_id UUID NOT NULL,
    game_id UUID NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id),
    FOREIGN KEY (game_id) REFERENCES games (id)
);

CREATE TABLE user_stats(
    user_id UUID PRIMARY KEY,
    wins INTEGER NOT NULL DEFAULT 0 CHECK (wins >= 0),
    losses INTEGER NOT NULL DEFAULT 0 CHECK (losses >= 0),
    games_count INTEGER NOT NULL DEFAULT 0 CHECK (games_count >= 0),
    FOREIGN KEY (user_id) REFERENCES users (id)
);