using BackendExamAPI.Data;
using BackendExamAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BackendExamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcpdController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AcpdController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BackendExamHub");
        }

        // GET: api/acpd
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyofficeAcpd>>> GetAcpds()
        {
            var acpds = new List<MyofficeAcpd>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Select_ACPD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var acpd = new MyofficeAcpd
                            {
                                AcpdSid = reader["acpd_sid"].ToString(),
                                AcpdCname = reader["acpd_cname"].ToString(),
                                AcpdEname = reader["acpd_ename"].ToString(),
                                AcpdSname = reader["acpd_sname"].ToString(),
                                AcpdEmail = reader["acpd_email"].ToString(),
                                AcpdStatus = reader["acpd_status"] as byte?,
                                AcpdStop = reader["acpd_stop"] as bool?,
                                AcpdStopMemo = reader["acpd_stopMemo"].ToString(),
                                AcpdLoginId = reader["acpd_LoginID"].ToString(),
                                AcpdLoginPw = reader["acpd_LoginPW"].ToString(),
                                AcpdMemo = reader["acpd_memo"].ToString()
                            };
                            acpds.Add(acpd);
                        }
                    }
                }
            }

            return Ok(acpds);
        }

        [HttpPost]
        [Route("api/acpd")]
        public async Task<IActionResult> PostAcpd([FromBody] MyofficeAcpd acpd)
        {
            if (acpd == null)
            {
                return BadRequest("Acpd data is required.");
            }

            try
            {
                string returnSid;
                string logInfo;

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // 建立命令使用儲存過程 Insert_ACPD
                    using (var command = new SqlCommand("Insert_ACPD", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 設定輸入參數
                        command.Parameters.AddWithValue("@acpd_cname", acpd.AcpdCname ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_ename", acpd.AcpdEname ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_sname", acpd.AcpdSname ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_email", acpd.AcpdEmail ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_status", acpd.AcpdStatus);
                        command.Parameters.AddWithValue("@acpd_stop", acpd.AcpdStop);
                        command.Parameters.AddWithValue("@acpd_stopMemo", acpd.AcpdStopMemo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_LoginID", acpd.AcpdLoginId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_LoginPW", acpd.AcpdLoginPw ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_memo", acpd.AcpdMemo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@acpd_nowid", acpd.AppdNowid ?? (object)DBNull.Value);

                        // 設定輸出參數
                        var returnSidParam = new SqlParameter("@ReturnSID", SqlDbType.NVarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(returnSidParam);

                        // 執行命令
                        await command.ExecuteNonQueryAsync();

                        // 獲取輸出參數的值
                        returnSid = returnSidParam.Value.ToString();
                    }

                    // 在返回時插入的 SID
                    return Ok(new { NewSID = returnSid });
                }
            }
            catch (Exception ex)
            {
                // 捕獲異常並返回錯誤信息
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // PUT: api/acpd/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcpd(string id, [FromBody] MyofficeAcpd acpd)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Update_ACPD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@acpd_sid", id);
                    command.Parameters.AddWithValue("@acpd_cname", acpd.AcpdCname);
                    command.Parameters.AddWithValue("@acpd_ename", acpd.AcpdEname);
                    command.Parameters.AddWithValue("@acpd_sname", acpd.AcpdSname);
                    command.Parameters.AddWithValue("@acpd_email", acpd.AcpdEmail);
                    command.Parameters.AddWithValue("@acpd_status", acpd.AcpdStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@acpd_stop", acpd.AcpdStop ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@acpd_stopMemo", acpd.AcpdStopMemo);
                    command.Parameters.AddWithValue("@acpd_LoginID", acpd.AcpdLoginId);
                    command.Parameters.AddWithValue("@acpd_LoginPW", acpd.AcpdLoginPw);
                    command.Parameters.AddWithValue("@acpd_memo", acpd.AcpdMemo);
                    command.Parameters.AddWithValue("@acpd_nowid", acpd.AppdNowid);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return NoContent();
        }

        // DELETE: api/acpd/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcpd(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Delete_ACPD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@acpd_sid", id);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return NoContent();
        }
    }    
}
