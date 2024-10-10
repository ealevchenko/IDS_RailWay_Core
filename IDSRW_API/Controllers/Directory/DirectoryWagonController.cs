using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Repositories;
using WebAPI.Repositories.Directory;
using Microsoft.Data.SqlClient;

namespace WebAPI.Controllers.Directory
{
    public class current_operation_wagon
    {
        public int num { get; set; }
        public int? id_wagons_rent { get; set; }
        public int? curr_wagons_rent_id_operator { get; set; }
        public string curr_wagons_rent_operators_ru { get; set; }
        public string curr_wagons_rent_operators_en { get; set; }
        public string curr_wagons_rent_operator_abbr_ru { get; set; }
        public string curr_wagons_rent_operator_abbr_en { get; set; }
        public DateTime? curr_wagons_rent_start { get; set; }
        public DateTime? curr_wagons_rent_end { get; set; }
        public bool? curr_wagons_rent_operator_paid { get; set; }
        public string curr_wagons_rent_operator_color { get; set; }
        public int? curr_wagons_rent_id_limiting { get; set; }
        public string curr_wagons_rent_limiting_name_ru { get; set; }
        public string curr_wagons_rent_limiting_name_en { get; set; }
        public string curr_wagons_rent_limiting_abbr_ru { get; set; }
        public string curr_wagons_rent_limiting_abbr_en { get; set; }
        public int wagon_id_countrys { get; set; }
        public int? wagon_adm { get; set; }
        public string wagon_adm_name_ru { get; set; }
        public string wagon_adm_name_en { get; set; }
        public string wagon_adm_abbr_ru { get; set; }
        public string wagon_adm_abbr_en { get; set; }
        public int wagon_id_genus { get; set; }
        public int? wagon_rod { get; set; }
        public string wagon_rod_name_ru { get; set; }
        public string wagon_rod_name_en { get; set; }
        public string wagon_rod_abbr_ru { get; set; }
        public string wagon_rod_abbr_en { get; set; }
        public int wagon_id_owner { get; set; }
        public string wagon_owner_wagon_ru { get; set; }
        public string wagon_owner_wagon_en { get; set; }
        public string wagon_owner_wagon_abbr_ru { get; set; }
        public string wagon_owner_wagon_abbr_en { get; set; }
        public double wagon_gruzp { get; set; }
        public double? wagon_tara { get; set; }
        public int wagon_kol_os { get; set; }
        public string wagon_usl_tip { get; set; }
        public DateTime? wagon_date_rem_uz { get; set; }
        public DateTime? cwagon_date_rem_vag { get; set; }
        public int? wagon_id_type_ownership { get; set; }
        public int? cwagon_sign { get; set; }
        public string wagon_factory_number { get; set; }
        public string wagon_inventory_number { get; set; }
        public int? wagon_year_built { get; set; }
        public bool? wagon_exit_ban { get; set; }
        public string wagon_note { get; set; }
        public int? wagon_sobstv_kis { get; set; }
        public bool? wagon_bit_warning { get; set; }
        public DateTime wagon_create { get; set; }
        public string wagon_create_user { get; set; }
        public DateTime? wagon_change { get; set; }
        public string wagon_change_user { get; set; }
        public bool? wagon_closed_route { get; set; }
        public string wagon_new_construction { get; set; }
        public long? wir_id { get; set; }
        public long? wir_id_arrival_car { get; set; }
        public long? wir_id_outgoing_car { get; set; }
        public string wir_note { get; set; }
        public string wir_highlight_color { get; set; }
        public int? arrival_id_condition { get; set; }
        public string arrival_condition_name_ru { get; set; }
        public string arrival_condition_name_en { get; set; }
        public string arrival_condition_abbr_ru { get; set; }
        public string arrival_condition_abbr_en { get; set; }
        public bool? arrival_condition_repairs { get; set; }
        public int? current_id_condition { get; set; }
        public string current_condition_name_ru { get; set; }
        public string currentn_condition_name_en { get; set; }
        public string current_condition_abbr_ru { get; set; }
        public string current_condition_abbr_en { get; set; }
        public bool? current_condition_repairs { get; set; }
        public DateTime? current_condition_create { get; set; }
        public string current_condition_create_user { get; set; }
        public string instructional_letters_num { get; set; }
        public DateTime? instructional_letters_datetime { get; set; }
        public int? instructional_letters_station_code { get; set; }
        public string instructional_letters_station_name { get; set; }
        public string instructional_letters_note { get; set; }
        public DateTime? cur_date_adoption { get; set; }
        public DateTime? cur_date_adoption_act { get; set; }
        public DateTime? cur_date_outgoing { get; set; }
        public DateTime? cur_date_outgoing_act { get; set; }
        public DateTime? last_date_outgoing { get; set; }
        public DateTime? last_date_outgoing_act { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class DirectoryWagonController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryWagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWagon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWagon>>> GetDirectoryWagon()
        {
            return await db.DirectoryWagons.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryWagon
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryWagon>>> GetListDirectoryWagon()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWagon> result = await db.DirectoryWagons.FromSql($"select * from [IDS].[Directory_Wagons]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
                if (result == null)
                    return NotFound();
                //db.Database.CommandTimeout = null;               
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryWagon/[num]
        [HttpGet("num/{num}")]
        public async Task<ActionResult<DirectoryWagon>> GetDirectoryWagon(int num)
        {
            DirectoryWagon result = await db.DirectoryWagons.AsNoTracking().FirstOrDefaultAsync(x => x.Num == num);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }

        // GET: DirectoryWagon/CurrentOperationWagon/num/65201857
        [HttpGet("CurrentOperationWagon/num/{num}")]
        public async Task<ActionResult<current_operation_wagon>> GetCurrentOperationWagonOfNum(int num)
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                SqlParameter param = new SqlParameter("@num", num);

                var result = await db.Database.SqlQueryRaw<current_operation_wagon>($"select * from [IDS].[get_current_operation_wagon_of_num]({num})").FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                //db.Database.CommandTimeout = null;               
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// POST: DirectoryWagon
        //// BODY: DirectoryWagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWagon>> PostDirectoryWagon([FromBody] DirectoryWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWagon/
        //// BODY: DirectoryWagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWagon>> PutDirectoryWagon(DirectoryWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWagon/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryWagon>> DeleteDirectoryWagon(int num)
        //{
        //    DirectoryWagon result = db.DirectoryWagons.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
