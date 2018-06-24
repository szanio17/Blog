CREATE TABLE BlogUser (
    id int PRIMARY KEY,
    first_name varchar(50) NOT NULL,
    last_name varchar(50) NOT NULL,
    nick varchar(50) NOT NULL,
    email varchar(50) NOT NULL,
    password varchar(50) NOT NULL,
    role_id int FOREIGN KEY references BlogRole(id)
);
drop table Article
create table BlogRole(
	id int primary key,
	identificator varchar(50)
);

CREATE TABLE Article (
    id int IDENTITY(1,1) PRIMARY KEY,
    title varchar(100) NOT NULL,
    author_id int NOT NULL,
    author_name varchar(100) NOT NULL,
    content varchar(2000) NOT NULL,
);

CREATE TABLE Comment (
    id int IDENTITY(1,1) PRIMARY KEY,
    author_nick varchar(50) NOT NULL,
    author_email varchar(50) NOT NULL,
    content varchar(4000) NOT NULL,
    ip_address varchar(35),
    user_agent varchar(150),
    article_id int FOREIGN KEY REFERENCES Article(ID)
);

ALTER TABLE Comment
alter column user_agent varchar(400);
insert into BlogUser(id, first_name, last_name, nick, email, password, role_id) values(1,'Karel', 'Prvni', 'Karlito', 'dany.mrozek@gmail.com', 'pass_user', 1); 
insert into BlogUser(id, first_name, last_name, nick, email, password, role_id) values(2,'Karel', 'Druhy', 'KarlitoZwei', 'dany.mrozek@gmail.com', 'pass_admin', 2); 

insert into BlogRole(id, identificator) values(1,'User')
insert into BlogRole(id, identificator) values(2,'Admin')

insert into Article(Title, AuthorID, AuthorName, Content) values('Prvni clanek', 1, 'Karel Prvni', 'Toto je muj prvni clanek');

insert into Comment(AuthorNick, AuthorEmail, Content, ArticleID) values('Pavlik01', 'nejaky.email@email.cz', 'Moc hezky clanek', 1);

select a.ID,a.Title,b.ID,b.AuthorNick,b.AuthorEmail,b.Content from Article a join Comment b on a.ID = b.ArticleID where a.ID=1

update Comment set IpAddress = 'null', UserAgent = 'null' where ID = 1

exec sp_rename 'Comment.ID', 'id', 'COLUMN'

select * from Comment

