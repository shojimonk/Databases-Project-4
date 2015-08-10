--This File to be run in PSQL in admin mode.

drop database if exists "LU.ENGI3675.Proj04";

create database "LU.ENGI3675.Proj04";

drop role if exists "Shoji";
create role "Shoji" login superuser;
comment on role "Shoji" is 'Personal developer superuser';

drop role if exists "George";
create role "George" login superuser;
comment on role "George" is 'Personal developer superuser';

drop role if exists "DefaultAppPool";
create role "DefaultAppPool" login;
comment on role "DefaultAppPool" is 'Restricted IIS app pool user';

drop role if exists "LU.ENGI3675";
create role "LU.ENGI3675" login;
comment on role "LU.ENGI3675" is 'Restricted IIS app pool user';

grant connect on database "LU.ENGI3675.Proj04" to "LU.ENGI3675";  --still doesn't allow any read/write. only connection.
grant connect on database "LU.ENGI3675.Proj04" to "DefaultAppPool";

\connect LU.ENGI3675.Proj04

drop table if exists Students cascade;

create table Students(
	id serial primary key,
	name text not null check(length(name) > 0),
	GPA double precision not null check(GPA between 0 and 4)
);

grant select, insert, update, delete on table Students to "LU.ENGI3675";
grant select, insert, update, delete on table Students to "DefaultAppPool";
grant usage on students_id_seq to "DefaultAppPool";