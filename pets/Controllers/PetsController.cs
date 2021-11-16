using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using pets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private string _connection = @"Server=192.168.1.2;Database=so;Uid=so;Password=12345678";
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<pet> lst = null;
            using (var db = new MySqlConnection(_connection))
            {

                var sql = "select id,name,age from pets";
                lst = db.Query<pet>(sql);

            }
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult Insert(pet pet)
        {
            int result=0;
            using (var db = new MySqlConnection(_connection))
            {

                var sql = "insert into pets (name,age) values(@name,@age)";
                result = db.Execute(sql, pet);
                    
                

            }
            return Created("Se ha creado",result);
        }

        [HttpPut]
        public IActionResult Edit(pet pet)
        {
            int resilt = 0;
            using (var db = new MySqlConnection(_connection))
            {
                var sql =$"update pets set name=@name,age=@age where id=@id";
                resilt = db.Execute(sql,pet);
            }

            return Ok(resilt);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            int resilt = 0;
            using (var db = new MySqlConnection(_connection))
            {
                var sql = $"delete from pets where id={id}";
                resilt = db.Execute(sql, id);
            }

            return Ok(resilt);
        }
    }
}
