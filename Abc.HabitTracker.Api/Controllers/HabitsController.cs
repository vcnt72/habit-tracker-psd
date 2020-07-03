using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.HabitTracker.Api.Database;
using Abc.HabitTracker.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Abc.HabitTracker.Api.Controllers
{
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly ILogger<HabitsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly HabitApiMapper _habitApiMapper;
        public HabitsController(ILogger<HabitsController> logger, IUnitOfWork unitOfWork, HabitApiMapper habitApiMapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _habitApiMapper = habitApiMapper;
        }

        [HttpGet("api/v1/users/{userID}/habits")]
        public ActionResult<IEnumerable<HabitJson>> All(Guid userID)
        {
            //mock only. replace with your solution
            List<Habit> habits = this._unitOfWork.HabitRepository.FindByUserId(userID);

            Console.WriteLine(habits);
            return this._habitApiMapper.MapMany(habits);
        }

        [HttpGet("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<HabitJson> Get(Guid userID, Guid id)
        {
            //mock only. replace with your solution
            Habit habit = this._unitOfWork.HabitRepository.FindById(id);
            return this._habitApiMapper.Map(habit);
        }

        [HttpPost("api/v1/users/{userID}/habits")]
        public ActionResult<HabitJson> AddNewHabit(Guid userID, [FromBody] RequestData data)
        {

            Habit habit = Habit.createHabit(data.Name, data.DaysOff, userID);
            try
            {
                this._unitOfWork.HabitRepository.Create(habit);
                this._unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                this._unitOfWork.Rollback();
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return this._habitApiMapper.Map(habit);
        }

        [HttpPut("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<HabitJson> UpdateHabit(Guid userID, Guid id, [FromBody] RequestData data)
        {

            Habit habit = new Habit(id, data.Name, DaysOff.createDaysOff(data.DaysOff), userID);

            try
            {

                this._unitOfWork.HabitRepository.Put(id, habit);
                this._unitOfWork.Commit();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                this._unitOfWork.Rollback();
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }



            return this._habitApiMapper.Map(habit);
        }

        [HttpDelete("api/v1/users/{userID}/habits/{id}")]
        public ActionResult<HabitJson> DeleteHabit(Guid userID, Guid id)
        {
            //mock only. replace with your solution

            Habit habit;

            try
            {
                habit = this._unitOfWork.HabitRepository.FindById(id);
                this._unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                this._unitOfWork.Rollback();
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }


            this._unitOfWork.HabitRepository.Delete(habit);
            return this._habitApiMapper.Map(habit);
        }

        [HttpPost("api/v1/users/{userID}/habits/{id}/logs")]
        public ActionResult<HabitJson> Log(Guid userID, Guid id)
        {
            //mock only. replace with your solution
            Habit habit = this._unitOfWork.HabitRepository.FindById(id);


            try
            {
                habit.DoLogs();
                this._unitOfWork.HabitRepository.DoLogs(habit);

                this._unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                this._unitOfWork.Rollback();
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            this._unitOfWork.HabitRepository.DoLogs(habit);
            return this._habitApiMapper.Map(habit);
        }

        //mock data only. remove later
        private static readonly Guid AmirID = Guid.Parse("4fbb54f1-f340-441e-9e57-892329464d56");
        private static readonly Guid BudiID = Guid.Parse("0b54c1fe-a374-4df8-ba9a-0aa7744a4531");

        //mock data only. remove later
        private static readonly HabitJson habitAmir1 = new HabitJson
        {
            ID = Guid.Parse("fd725b05-a221-461a-973c-4a0899cee14d"),
            Name = "baca buku",
            UserID = AmirID
        };

        //mock data only. remove later
        private static readonly HabitJson habitAmir2 = new HabitJson
        {
            ID = Guid.Parse("01169031-752e-4c52-822c-a04d290438ea"),
            Name = "code one simple app prototype",
            DaysOff = new[] { "Sat", "Sun" },
            UserID = AmirID
        };

        //mock data only. remove later
        private static readonly HabitJson habitBudi1 = new HabitJson
        {
            ID = Guid.Parse("05fb5a61-aa1f-4a96-b952-378bf73ca713"),
            Name = "100 push-ups, 100 sit-ups, 100 squats",
            LongestStreak = 100,
            CurrentStreak = 10,
            LogCount = 123,
            UserID = BudiID
        };


        private string[] formatDaysOff(DaysOff daysOff)
        {
            return daysOff.Days.Select(e => e.Name).ToArray();
        }
    }
}
