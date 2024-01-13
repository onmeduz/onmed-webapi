FROM postgres:latest

ENV POSTGRES_USER postgres_admin
ENV POSTGRES_PASSWORD AAaa@@11
ENV POSTGRES_DB onmed-db
COPY ./apps/postgres/init.sql /docker-entrypoint-initdb.d/