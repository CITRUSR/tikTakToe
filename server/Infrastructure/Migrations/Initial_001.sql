CREATE TABLE users(
    id SERIAL PRIMARY KEY,
    nickname VARCHAR(128) NOT NULL,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE rooms(
    id SERIAL PRIMARY KEY,
    user1_id INTEGER,
    user2_id INTEGER,
    FOREIGN KEY (user1_id) REFERENCES users(id),
    FOREIGN KEY (user2_id) REFERENCES users(id)
);

CREATE TABLE games(
    id UUID PRIMARY KEY NOT NULL,
    user1_id INTEGER NOT NULL,
    user2_id INTEGER NOT NULL,
    winner_id INTEGER,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    FOREIGN KEY (user1_id) REFERENCES users (id),
    FOREIGN KEY (user2_id) REFERENCES users (id)
);

CREATE TABLE user_stats(
    user_id INTEGER PRIMARY KEY,
    wins INTEGER NOT NULL DEFAULT 0 CHECK (wins >= 0),
    losses INTEGER NOT NULL DEFAULT 0 CHECK (losses >= 0),
    games_count INTEGER NOT NULL DEFAULT 0 CHECK (games_count >= 0),
    FOREIGN KEY (user_id) REFERENCES users (id)
);