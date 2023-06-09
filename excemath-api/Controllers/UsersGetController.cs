﻿/* excemath - an app for preparing for math exams.
* Copyright (C) 2023 miu-miu enjoyers

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program. If not, see <https://www.gnu.org/licenses/>. */

using excemathApi.Data;
using excemathApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace excemathApi.Controllers;

/// <summary>
/// Представляє контролер для контексту бази даних <see cref="UsersApiDbContext"/>, який дозволяє надсилати запити отримання.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersGetController : Controller
{
    #region Поля

    private readonly UsersApiDbContext _dbContext;

    #endregion

    #region Конструктори

    /// <summary>
    /// Створює екземпляр класу <see cref="UsersGetController"/>, використовуючи вказаний контекст бази даних.
    /// </summary>
    /// <param name="dbContext">Контекст бази даних.</param>
    public UsersGetController(UsersApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Методи

    /// <summary>
    /// Дозволяє клієнту отримати рейтинговий список користувачів.
    /// </summary>
    /// <remarks>
    /// Рейтинг обчислюється наступним чином:    
    /// <code>
    /// double rating = ((User.RightAnswers + User.WrongAnswers) > 0) ? ((double)User.RightAnswers / (User.RightAnswers + User.WrongAnswers)) : 0;
    /// </code>
    /// </remarks>
    /// <returns>
    /// Рейтинговий список користувачів як список <see cref="List{T}"/> розміром 10 елементів з <see cref="UserRating"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>).
    /// </returns>
    [HttpGet]
    [Route("rating_list")]
    public async Task<IActionResult> GetRatingList()
    {
        List<User> users = await _dbContext.Users.ToListAsync();

        List<UserRating> ratingList = users.Select(u =>
        {
            double rating = ((u.RightAnswers + u.WrongAnswers) > 0) ? ((double)u.RightAnswers * 100 / (u.RightAnswers + u.WrongAnswers)) : 0;

            return new UserRating
            {
                Nickname = u.Nickname,
                Rating = rating
            };
        }).OrderByDescending(u => u.Rating).ToList();
        ratingList.ForEach(u => u.Rating = Math.Round(u.Rating, 2));

        int index = 10;        

        if (ratingList.Count >= index)
        {
            int count = ratingList.Count - index;
            ratingList.RemoveRange(index, count);
        }            

        return Ok(ratingList);
    }

#nullable enable

    /// <summary>
    /// Дозволяє клієнту отримати конкретний об'єкт класу <see cref="UserGetRequest"/> за вказаним псевдонімом.
    /// </summary>
    /// <param name="nickname">Псевдонім користувача.</param>
    /// <returns>
    /// Якщо об'єкт знайдено, його як <see cref="UserGetRequest"/> (інтегрований у HTTP-відповідь <see cref="OkObjectResult"/>); інакше, HTTP-відповідь <see cref="NotFoundObjectResult"/>.
    /// </returns>
    [HttpGet]
    [Route("nickname")]
    public async Task<IActionResult> GetUser([FromQuery] string nickname)
    {
        User? user = await _dbContext.Users.FindAsync(nickname);

        if (user is null)
            return NotFound();

        return Ok(new UserGetRequest
        {
            Nickname = user.Nickname,
            RightAnswers = user.RightAnswers,
            WrongAnswers = user.WrongAnswers
        });
    }

    #endregion
}
