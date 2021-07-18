drop database if exists catalogo_jogos;
create database catalogo_jogos;
use catalogo_jogos;

create table Producers
(
	Id int primary key auto_increment,
    Name varchar(100)
);

create table Games (
	Id int primary key auto_increment,
    ProducerId int,
    Name varchar(100),
    Price decimal(10, 2),
    foreign key (ProducerId) references Producers(Id)
);