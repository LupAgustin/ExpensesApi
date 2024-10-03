select * from Users
select * from Categories
select * from expenses


select u.Username, e.Amount, c.Name, e.Date from Users u
 join Expenses e on e.UserId = u.Id
 join Categories c on e.CategoryId = c.Id

select u.id from Users u
 left join Expenses e on e.UserId = u.Id
 where e.id is NULL

 delete Users
 where Users.id in (select u.id from Users u
 left join Expenses e on e.UserId = u.Id
 where e.id is NULL)