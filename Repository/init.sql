create extension if not exists pg_trgm;

--- Информация о человеке
create table if not exists person
(
    id    bigint GENERATED ALWAYS AS IDENTITY,
    email varchar NOT NULL,
    PRIMARY KEY (id)
);

--- Категория, к которой относится анкета
create table if not exists category
(
    id   bigint GENERATED ALWAYS AS IDENTITY,
    name varchar NOT NULL,
    PRIMARY KEY (id)
);

create index if not exists idx_category_name ON category using gin (name gin_trgm_ops);

--- Информация об анкете
create table if not exists survey
(
    id              bigint GENERATED ALWAYS AS IDENTITY,
    category_id     bigint  NOT NULL,
    name            varchar NOT NULL,
    question_number int     NOT NULL,
    description     varchar NOT NULL,
    active          bool    NOT NULL,
    only_once       bool    NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (category_id) REFERENCES category (id)
);

create index if not exists idx_survey_category_id ON survey (category_id);

--- Информация об интервью (сессия)
create table if not exists interview
(
    id         bigint GENERATED ALWAYS AS IDENTITY,
    survey_id  bigint      NOT NULL,
    person_id  bigint,
    start_time timestamptz NOT NULL,
    end_time   timestamptz,
    PRIMARY KEY (id),
    FOREIGN KEY (survey_id) REFERENCES survey (id),
    FOREIGN KEY (survey_id) REFERENCES person (id)
);

create index if not exists idx_interview_survey_id ON interview (survey_id);
create index if not exists idx_interview_person_id ON interview (person_id);

--- Вопрос анкеты
create table if not exists question
(
    id                   bigint GENERATED ALWAYS AS IDENTITY,
    survey_id            bigint  NOT NULL,
    mandatory            bool    NOT NULL,
    allowed_many_answers bool    NOT NULL,
    orderNo              int     NOT NULL,
    name                 varchar NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (survey_id) REFERENCES survey (id)
);

create index if not exists idx_question_survey_id ON question (survey_id);
create unique index if not exists idx_question_orderno ON question (orderNo ASC);

--- Вариант ответа на вопрос
create table if not exists answer
(
    id          bigint GENERATED ALWAYS AS IDENTITY,
    question_id bigint  NOT NULL,
    content     varchar NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (question_id) REFERENCES question (id)
);

create index if not exists idx_answer_question_id ON answer (question_id);

--- Данные ответов людей на вопросы анкеты
create table if not exists result
(
    id           bigint GENERATED ALWAYS AS IDENTITY,
    interview_id bigint NOT NULL,
    answer_id    bigint NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (interview_id) REFERENCES interview (id),
    FOREIGN KEY (answer_id) REFERENCES answer (id)
);

create index if not exists idx_result_interview_id ON result (interview_id);
create index if not exists idx_result_answer_id ON result (answer_id);


--- Популяция БД
INSERT INTO public.person (email)
VALUES ('test@mail.ru');

INSERT INTO public.category (name)
VALUES ('Тест');
INSERT INTO public.category (name)
VALUES ('Срочные');
INSERT INTO public.category (name)
VALUES ('Свалка');

INSERT INTO public.survey (name, category_id, question_number, description, active, only_once)
VALUES ('Моя первая анкета', 1, 2, 'Тестовая анкета', true, false);

INSERT INTO public.question (survey_id, orderNo, mandatory, allowed_many_answers, name)
VALUES (1, 1, true, false, 'Из какого Вы города?');

INSERT INTO public.question (survey_id, orderNo, mandatory, allowed_many_answers, name)
VALUES (1, 2, true, false, 'Вы любите С#?');

INSERT INTO public.interview (survey_id, person_id, start_time, end_time)
VALUES (1, 1, '2024-02-12 20:47:09.620000 +00:00', null);

INSERT INTO public.answer (question_id, content)
VALUES (1, 'Санкт-Петербург');
INSERT INTO public.answer (question_id, content)
VALUES (1, 'Москва');
INSERT INTO public.answer (question_id, content)
VALUES (1, 'Новгород');
INSERT INTO public.answer (question_id, content)
VALUES (2, 'Да');
INSERT INTO public.answer (question_id, content)
VALUES (2, 'Нет');

