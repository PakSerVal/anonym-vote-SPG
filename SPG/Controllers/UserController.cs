﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SPG.Data;
using SPG.Models.Db;

namespace SPG.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private ElectContext electContext;

        public UserController(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        [HttpPost("get")]
        public IActionResult GetList()
        {
            return Ok(electContext.Users.ToList());
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (electContext.Users.FirstOrDefault(u => u.LIK == user.LIK) == null)
                {
                    electContext.Users.Add(user);
                    electContext.SaveChanges();
                    return Ok();
                }
                return BadRequest(new { message = "Такой пользователь уже зарегистрирован в системе" });
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (electContext.Users.FirstOrDefault(u => u.ID == user.ID) != null)
                {
                    electContext.Users.Update(user);
                    electContext.SaveChanges();
                    return Ok(user);
                }
                return BadRequest(new { message = "Такого пользователя не существует" });
            }
            return BadRequest(new { message = "Ошибка валидации" });
        }

        [HttpPost("delete/{userId}")]
        public IActionResult Delete([FromBody] int? userId)
        {
            if (userId != null)
            {
                User user = electContext.Users.FirstOrDefault(u => u.ID == userId);
                if (user != null)
                {
                    electContext.Users.Remove(user);
                    electContext.SaveChanges();
                    return Ok();
                }
                return BadRequest(new { message = "Пользователь с таким id не найден" });
            }
            return BadRequest(new { message = "id пользователя не указан" });
        }
    }
}