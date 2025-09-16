//create table users (
//id int generated always as identity primary key,
//profilepic jsonb not null ,
//email text not null,
//password text not null,
//dob timestamp without time zone  not null,
//age text not null,
//gender text not null,
//about text not null,
//hobbies text[] ,
//firstname text not null,
//middlename text not null,
//lastname text not null,
//phoneno text not null,
//address text not null,
//landmark text not null ,
//pincode text not null,
//country text not null,
//state text not null,
//city text not null,
//role text default 'admin',
//resetpasswordtoken text default 'value',
//resetpasswordexpiry timestamp without time zone default '2002-02-02'
//)


//create table student(
//sid int generated always as identity primary key,
//firstname text not null,
//lastname text not null,
//rollno int not null,
//studentclass text not null,
//presentdate timestamp without time zone not null,
//ispresent bool ,
//isabsent bool
//)

//create table attendance (
//aid int not null,
//firstname text not null,
//lastname text not null,
//rollno int not null,
//studentclass text not null,
//presentdate timestamp without time zone not null,
//ispresent bool ,
//isabsent bool
//)

//drop table attendance

//drop table student

//select * from student

//delete from  users where id <> 6


//insert into users (
//    profilepic, email, password, dob, age, gender, about, hobbies,
//    firstname, middlename, lastname, phoneno, address, landmark,
//    pincode, country, state, city, role, resetpasswordtoken, resetpasswordexpiry
//)
//values
//(
//    '{"avatar": "https://example.com/profilepic1.jpg"}',
//    'user1@example.com', 'password1', '1990-01-15', '31', 'male',
//    'lorem ipsum dolor sit amet.',
//    '{"reading", "traveling", "swimming"}', 'john', 'doe', 'smith',
//    '1234567890', '123 main st', 'near park', '10001', 'usa', 'california', 'los angeles',
//    'admin', null, null
//),
//(
//    '{"avatar": "https://example.com/profilepic2.jpg"}',
//    'user2@example.com', 'password2', '1985-07-23', '36', 'female',
//    'consectetur adipiscing elit.',
//    '{"photography", "cooking"}', 'jane', 'eve', 'doe',
//    '0987654321', '456 oak st', 'near school', '20002', 'usa', 'texas', 'dallas',
//    'admin', null, null
//),
//(
//    '{"avatar": "https://example.com/profilepic3.jpg"}',
//    'user3@example.com', 'password3', '2000-12-10', '20', 'non-binary',
//    'sed do eiusmod tempor incididunt.',
//    '{"gaming", "music"}', 'alex', 'taylor', 'lee',
//    '1122334455', '789 pine st', 'near mall', '30003', 'usa', 'new york', 'new york',
//    'user', null, null
//),
//(
//    '{"avatar": "https://example.com/profilepic4.jpg"}',
//    'user4@example.com', 'password4', '1995-11-05', '25', 'female',
//    'ut labore et dolore magna aliqua.',
//    '{"yoga", "writing"}', 'emily', 'grace', 'brown',
//    '6677889900', '321 birch st', 'near cafe', '40004', 'usa', 'florida', 'miami',
//    'user', null, null
//),
//(
//    '{"avatar": "https://example.com/profilepic5.jpg"}',
//    'user5@example.com', 'password5', '1998-02-19', '23', 'male',
//    'ut enim ad minim veniam.',
//    '{"cycling", "hiking"}', 'michael', 'james', 'wilson',
//    '2233445566', '654 cedar st', 'near station', '50005', 'usa', 'illinois', 'chicago',
//    'user', null, null
//);


//select* from users

//--------------------------------------------get users with pagination--------------------------------------------

//create or replace function public.get_user_values(page INT, pageSize Int default 1, totalPages int default 5, sortByName text default 'id', sortByType text default 'desc', searchByName text default 'id', StartDate text default '', EndDate text default '')
//returns table(id1 int, profilepic1 jsonb, email1 text, password1 text, dob1 text, age1 text, gender1 text, about1 text, hobbies1 text[],
//    firstname1 text, middlename1 text, lastname1 text, phoneno1 text, address1 text, landmark1 text,
//    pincode1 text, country1 text, state1 text, city1 text, role1 text, resetpasswordtoken1 text, resetpasswordexpiry1 text)
// language plpgsql
// as $$
// begin
// 	return query Execute format('select id,profilepic, email, password, dob::text, age, gender, about, hobbies, 
//    firstname, middlename, lastname, phoneno, address, landmark,
//    pincode, country, state, city, role, resetpasswordtoken, resetpasswordexpiry::text from users  %s %s Limit %s offset %s',


//case 
//     WHEN searchByName IS NOT NULL AND searchByName <> '' THEN
//       format('WHERE LOWER(email) LIKE %L OR Lower(firstname) LIKE %L ', '%%' || LOWER(searchByName) || '%%','%%' || LOWER(searchByName) || '%%')
//       ELSE ''
//    END,
//CASE 
//    WHEN sortByName IS NOT NULL AND sortByType IS NOT NULL THEN
//        format('ORDER BY %I %s',
//            CASE 
//                WHEN sortByName = 'id' THEN 'id'
//                WHEN sortByName = 'email' THEN 'email'
//                WHEN sortByName = 'dob' THEN 'dob'
//                WHEN sortByName = 'age' THEN 'age'
//                WHEN sortByName = 'gender' THEN 'gender'
//                WHEN sortByName = 'about' THEN 'about'
//                WHEN sortByName = 'fullname' THEN 'firstname'
//                WHEN sortByName = 'country' THEN 'country'
//                WHEN sortByName = 'address' THEN 'address'
//                WHEN sortByName = 'landmark' THEN 'landmark'
//                ELSE 'id'
//              END,
//            CASE 
//               WHEN LOWER(sortByType) = 'asc' THEN 'ASC'
//               ELSE 'DESC'
//               END )
//            ELSE''
//END,
// pageSize::int,(page - 1) * pageSize::int  
// );

//end;
//$$
// DROP FUNCTION IF EXISTS public.get_user_values;

//drop function public.get_user_values()

//--------------------------------------------get users without pagination--------------------------------------------

// create or replace function public.getall_user_values()
//returns table(id1 int, profilepic1 jsonb, email1 text, password1 text, dob1 text, age1 text, gender1 text, about1 text, hobbies1 text[],
//    firstname1 text, middlename1 text, lastname1 text, phoneno1 text, address1 text, landmark1 text,
//    pincode1 text, country1 text, state1 text, city1 text, role1 text, resetpasswordtoken1 text, resetpasswordexpiry1 text)
// language plpgsql
// as $$
// begin
// 	return query select id, profilepic, email, password, dob::text, age, gender, about, hobbies,
//    firstname, middlename, lastname, phoneno, address, landmark,
//    pincode, country, state, city, role, resetpasswordtoken, resetpasswordexpiry::text from users;
//end;
//$$
//DROP FUNCTION IF EXISTS public.getall_user_values();
//select* from public.getAll_user_values()
//SELECT proname, proargtypes FROM pg_proc WHERE proname = 'get_user_values';
//SELECT current_database();



//--------------------------------------------get user with id--------------------------------------------

    
//create or replace function public.get_user_byid(uid int)
//returns table(id1 int, profilepic1 jsonb, email1 text, password1 text, dob1 text, age1 text, gender1 text, about1 text, hobbies1 text[],
//    firstname1 text, middlename1 text, lastname1 text, phoneno1 text, address1 text, landmark1 text,
//    pincode1 text, country1 text, state1 text, city1 text, role1 text, resetpasswordtoken1 text, resetpasswordexpiry1 text)
// language plpgsql
// as $$
// begin
// 	return query select id, profilepic, email, password, dob::text, age, gender, about, hobbies,
//    firstname, middlename, lastname, phoneno, address, landmark,
//    pincode, country, state, city, role, resetpasswordtoken, resetpasswordexpiry::text from users 
//    where id = uid;
//end;
//$$


// drop function public.get_user_byid
// select * from  public.get_user_byid(1);

//--------------------------------------------Create user--------------------------------------------



// create or replace function public.create_user(profilepic jsonb, email text, password text, dob text, age text, gender text, about text, hobbies text[],
//    firstname text, middlename text, lastname text, phoneno text, address text, landmark text,
//    pincode text, country text, state text, city text, role text, resetpasswordtoken text, resetpasswordexpiry text)
//returns void
// language plpgsql
// as $$
//declare
//	n_dob timestamp without time zone;
//n_resetpasswordexpiry timestamp without time zone;

//begin
//    n_dob := dob::timestamp without time zone;
//n_resetpasswordexpiry:= resetpasswordexpiry::timestamp without time zone;
//insert into users (
//    profilepic, email, password, dob, age, gender, about, hobbies,
// firstname, middlename, lastname, phoneno, address, landmark,
// pincode, country, state, city, role, resetpasswordtoken, resetpasswordexpiry
//) values ( profilepic, email, password, n_dob, age, gender, about, hobbies,
// firstname, middlename, lastname, phoneno, address, landmark,
// pincode, country, state, city, role, resetpasswordtoken, n_resetpasswordexpiry);
//end;
//$$
// drop function public.create_user
// select * from public.create_user('{"sham": "https://example.com/profilepic5.jpg"}',
//    'user5@example.com', 'password5', '1998-02-19', '23', 'male',
//    'ut enim ad minim veniam.',
//    '{"cycling", "hiking"}', 'michael', 'james', 'wilson',
//    '2233445566', '654 cedar st', 'near station', '50005', 'usa', 'illinois', 'chicago',
//    'user', null, null)



//    --------------------------------------------Update User--------------------------------------------

    
// create or replace function public.update_user(u_id int, u_profilepic jsonb, u_email text, u_password text, u_dob text, u_age text, u_gender text, u_about text, u_hobbies text[],
//    u_firstname text, u_middlename text, u_lastname text, u_phoneno text, u_address text, u_landmark text,
//    u_pincode text, u_country text, u_state text, u_city text, u_role text, u_resetpasswordtoken text, u_resetpasswordexpiry text)
//returns void
// language plpgsql
// as $$
//declare
//	n_dob timestamp without time zone;
//n_resetpasswordexpiry timestamp without time zone;

//begin
//    n_dob := u_dob::timestamp without time zone;
//n_resetpasswordexpiry:= u_resetpasswordexpiry::timestamp without time zone;

//update users
//set profilepic=u_profilepic, email = u_email, password = u_password, dob = n_dob, age = u_age, gender = u_gender, about = u_about, hobbies = u_hobbies,
//    firstname = u_firstname, middlename = u_middlename, lastname = u_lastname, phoneno = u_phoneno, address = u_address, landmark = u_landmark,
//    pincode = u_pincode, country = u_country, state = u_state, city = u_city, role = u_role, resetpasswordtoken = u_resetpasswordtoken, resetpasswordexpiry = n_resetpasswordexpiry
//    where id = u_id;
//end;
//$$


// drop function public.update_user
 
// select * from public.update_user(6, '{"avtar": "https://example.com/profilepic5.jpg"}',
//    'user5@example.com', 'password5', '1998-02-19', '23', 'male',
//    'ut enim ad minim veniam.',
//    '{"cycling", "hiking"}', 'michael', 'james', 'wilson',
//    '2233445566', '654 cedar st', 'near station', '50005', 'usa', 'illinois', 'chicago',
//    'user', null, null)


//  select* from users 
    
//  --------------------------------------------Delete user--------------------------------------------

 
//create or replace function public.delete_user(u_id int)
//returns void
// language plpgsql
// as $$
// begin
//   delete from users where id = u_id;
//end;
//$$
// select* from public.delete_user(6)


// ------------------------------------student----------------------------------


// insert into student (
//    firstname, lastname, rollno, studentclass, presentdate, ispresent, isabsent
//)
//values
//('john', 'doe', 101, '10a', '2025-09-08', true, false),
//('jane', 'smith', 102, '10a', '2025-09-08', false, true),
//('alice', 'johnson', 103, '10b', '2025-09-08', true, false),
//('bob', 'williams', 104, '10b', '2025-09-08', false, true),
//('charlie', 'brown', 105, '10c', '2025-09-08', true, false);

//CREATE TYPE Design_Student AS (
//    page INT,
//    pageSize Int,
//    totalPages int,
//    sortByName text,
//    sortByType text,
//    searchByName text,
//    StartDate text,
//    EndDate text,
//    isPresent boolean,
//    isAbsent boolean  
// )
 
// --------------------------------------------get student with pagination--------------------------------------------

 
//CREATE OR REPLACE FUNCTION public.get_student_values(
//    page INT,
//    pageSize INT DEFAULT 1,
//    totalPages INT DEFAULT 5,
//    sortByName TEXT DEFAULT 'presentDate',
//    sortByType TEXT DEFAULT 'desc',
//    searchByName TEXT DEFAULT '',
//    StartDate TEXT DEFAULT '2020-02-02',
//    EndDate TEXT DEFAULT '2030-02-02',
//    isPresent BOOLEAN DEFAULT true,
//    isAbsent BOOLEAN DEFAULT true
//)
//RETURNS TABLE(
//    sid1 INT,
//    firstname1 TEXT,
//    lastname1 TEXT,
//    rollno1 INT,
//    studentclass1 TEXT,
//    presentdate1 TEXT,
//    ispresent1 BOOLEAN,
//    isabsent1 BOOLEAN
//)
//LANGUAGE plpgsql AS
//$$
//BEGIN
//RETURN QUERY
//EXECUTE format(
//   '
//    SELECT sid, firstname, lastname, rollno, studentclass, presentdate::text, ispresent, isabsent
//    FROM student
//    WHERE (%s)
//      AND presentdate::date BETWEEN %L AND %L
//      AND (%s)
//    %s
//    LIMIT %s OFFSET %s
//    ',
    
//    CASE 
//        WHEN searchByName IS NOT NULL AND searchByName <> '' THEN
//            format('LOWER(firstname) LIKE %L OR rollno::text LIKE %L',
//                   '%%' || LOWER(searchByName) || '%%',
//                   '%%' || LOWER(searchByName) || '%%')
//        ELSE
//            'TRUE'
//    END,


//    StartDate, EndDate,


//    CASE
//        WHEN isPresent AND isAbsent THEN 'true'
//        WHEN isPresent THEN 'ispresent = true'
//        WHEN isAbsent THEN 'isabsent = true'
//        ELSE 'false'
//    END,


//    CASE 
//        WHEN sortByName IS NOT NULL AND sortByType IS NOT NULL THEN
//            format('ORDER BY %I %s',
//                CASE 
//                    WHEN sortByName = 'fullname' THEN 'firstname'
//                    WHEN sortByName = 'lastname' THEN 'lastname'
//                    WHEN sortByName = 'RollNo' THEN 'rollno'
//                    WHEN sortByName = 'StudentClass' THEN 'studentclass'
//                    WHEN sortByName = 'presentDate' THEN 'presentdate'
//                    WHEN sortByName = 'id' THEN 'sid'
//                    ELSE 'presentdate'
//                END,
//                CASE 
//                    WHEN LOWER(sortByType) = 'asc' THEN 'ASC'
//                    ELSE 'DESC'
//                END)
//        ELSE ''
//    END,


//    pageSize,
//    (page - 1) * pageSize
//);
//END;
//$$;

//--------------------------------------------get student withonly pagination--------------------------------------------


//CREATE OR REPLACE FUNCTION public.get_student_onlypagination_values(
//    page INT,
//    pageSize INT DEFAULT 1
//)
//RETURNS TABLE(
//    sid1 INT,
//    firstname1 TEXT,
//    lastname1 TEXT,
//    rollno1 INT,
//    studentclass1 TEXT,
//    presentdate1 TEXT,
//    ispresent1 BOOLEAN,
//    isabsent1 BOOLEAN
//)
//LANGUAGE plpgsql AS
//$$
//BEGIN
//RETURN QUERY
//EXECUTE format(
//   '
//    SELECT sid, firstname, lastname, rollno, studentclass, presentdate::text, ispresent, isabsent
//    FROM student
//    LIMIT %s OFFSET %s
//    ',

//    pageSize,
//    (page - 1) * pageSize
//);
//END;
//$$;
//select* from public.get_student_onlypagination_values(2, 5);

//DROP FUNCTION IF EXISTS public.get_student_values;
//select* from public.get_student_values(1, 5, 0, '', 'Desc', '', '2025-01-01', '2025-09-25', true, false)
//select* from student

//--------------------------------------------get all student--------------------------------------------


//CREATE OR REPLACE FUNCTION public.getall_student_values()
//RETURNS TABLE(sid1 INT, firstname1 TEXT, lastname1 TEXT, rollno1 INT, studentclass1 TEXT, presentdate1 TEXT, ispresent1 BOOLEAN, isabsent1 BOOLEAN)
//LANGUAGE plpgsql AS
//$$
//BEGIN
//RETURN QUERY SELECT sid, firstname, lastname, rollno, studentclass, presentdate::text, ispresent, isabsent FROM student ;
//END;
//$$;

//--------------------------------------------get Student by id--------------------------------------------


// create or replace function public.get_student_byid(id int)
//returns table(sid1 int, firstname1 text, lastname1 text, rollno1 int, studentclass1 text, presentdate1 text, ispresent1 boolean, isabsent1 boolean)
// language plpgsql
// as $$
// begin
// 	return query select sid, firstname, lastname, rollno, studentclass, presentdate::text, ispresent, isabsent from student where sid = id;
//end;
//$$


// drop function public.get_student_byid
// select * from get_student_byid(1)
 
// select * from student
 
// --------------------------------------------Create Student--------------------------------------------

 
//  create or replace function public.create_student(firstname text, lastname text, rollno int, studentclass text, presentdate text, ispresent boolean, isabsent boolean)
//returns void
// language plpgsql
// as $$
//declare
//	n_presentdate timestamp without time zone;
//begin
//    n_presentdate := presentdate::timestamp without time zone;
//insert into student ( firstname, lastname, rollno, studentclass, presentdate, ispresent, isabsent)values(firstname, lastname, rollno, studentclass, n_presentdate, ispresent, isabsent);
//end;
//$$


//  drop function public.create_student
// select * from public.create_student('sham', 'doe', 101, '10a', '2025-09-08', true, false)

// --------------------------------------------Update Student--------------------------------------------

 
//create or replace function public.update_student(id1 int, firstname1 text, lastname1 text, rollno1 int, studentclass1 text, presentdate1 text, ispresent1 boolean, isabsent1 boolean)
//returns void
// language plpgsql
// as $$
//declare
//	n_presentdate timestamp without time zone;
//begin
//    n_presentdate := presentdate1::timestamp without time zone;
//update student
//    set firstname=firstname1, lastname = lastname1, rollno = rollno1, studentclass = studentclass1, presentdate = n_presentdate, ispresent = ispresent1, isabsent = isabsent1
//    where sid = id1;
//end;
//$$


// drop function public.update_student
// select * from update_student(2,'shinde', 'johnson', 103, '10b', '2025-09-08', true, false);

//select* from student
 
// --------------------------------------------Delete User--------------------------------------------

// create or replace function public.delete_student(id1 int)
//returns void
// language plpgsql
// as $$
// begin
// 	delete from student where sid = id1;
//end;
//$$



// select* from public.delete_student(5)
// SELECT* FROM pg_proc WHERE proname = 'create_student';



//-------------------------------------------------------attendance------------------------------------------ -
// --------------------------------------------Create Attendence--------------------------------------------



//   create or replace function public.create_attendance(aid int, firstname text, lastname text, rollno int, studentclass text, presentdate text, ispresent boolean, isabsent boolean)
//returns void
// language plpgsql
// as $$
//declare
//	n_presentdate timestamp without time zone;
//begin
//    n_presentdate := presentdate::timestamp without time zone;
//insert into attendance (aid, firstname, lastname, rollno, studentclass, presentdate, ispresent, isabsent)values(aid, firstname, lastname, rollno, studentclass, n_presentdate, ispresent, isabsent);
//end;
//$$
// drop function public.create_attendance


//--------------------------------------------get attendence with pagination--------------------------------------------

 
//CREATE OR REPLACE FUNCTION public.getall_attendance_values(
//    page INT,
//    pageSize INT DEFAULT 1,
//    attendancesearch TEXT DEFAULT NULL
//)
//RETURNS TABLE(
//    aid1 INT,
//    firstname1 TEXT,
//    lastname1 TEXT,
//    rollno1 INT,
//    studentclass1 TEXT,
//    presentdate1 TEXT,
//    ispresent1 BOOLEAN,
//    isabsent1 BOOLEAN
//)
//LANGUAGE plpgsql AS
//$$
//DECLARE
//    n_presentdate TIMESTAMP WITHOUT TIME ZONE;
//BEGIN

//    IF attendancesearch IS NOT NULL THEN
//        n_presentdate := attendancesearch::timestamp without time zone;
//END IF;


//RETURN QUERY
//    EXECUTE format(
//        '
//        SELECT aid, firstname, lastname, rollno, studentclass, presentdate::text, ispresent, isabsent
//        FROM attendance
//        WHERE (%L IS NULL OR presentdate = %L)
//        ORDER BY presentdate DESC
//        LIMIT %s OFFSET %s
//        ',
//        attendancesearch,
//    n_presentdate,
//    pageSize,
//        (page - 1) * pageSize
//    );
//END;
//$$;


//select* from public.getall_attendance_values(1, 5, '2025-09-14')
//drop function public.getall_attendance_values;
//--------------------------------------------get users without pagination--------------------------------------------


//CREATE OR REPLACE FUNCTION public.getall_attendance_values_without_pagination()
//RETURNS TABLE(
//    aid1 INT,
//    firstname1 TEXT,
//    lastname1 TEXT,
//    rollno1 INT,
//    studentclass1 TEXT,
//    presentdate1 TEXT,
//    ispresent1 BOOLEAN,
//    isabsent1 BOOLEAN
//)
//LANGUAGE plpgsql AS
//$$
//BEGIN
//    RETURN QUERY  SELECT aid, firstname, lastname, rollno, studentclass, presentdate::text, ispresent, isabsent
//        FROM attendance order by presentdate Desc;

//END;
//$$;
//select* from attendance
//select * from public.getall_attendance_values_without_pagination()
//drop function public.getall_attendance_values
    