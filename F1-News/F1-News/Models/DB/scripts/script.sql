create database f1DB collate utf8mb4_general_ci;

use f1DB;

create table user(
	user_id int unsigned not null auto_increment,
	username varchar(100) not null,
	password varchar (300) not null,
	firstname varchar (50) not null,
	lastname varchar (50) not null,
	email varchar (50) not null,
	birthdate date null,
	gender int null,
	constraint user_id_PK primary key (user_id)
);

insert into user values(null, "dolettenbichler", sha2("domi", 512), "Dominik", "Lettenbichler", "dolettenbichler@tsn.at", "2004-01-01", 0);
