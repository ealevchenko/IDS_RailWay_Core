using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIVC
{
    public class gruz_detali
    {
        public string? etsng { get; set; }
        public string? nvs { get; set; }
    }
    public class st_otpr_detali
    {
        public int? esr_otpr { get; set; }
        public string? n_rpus { get; set; }
    }
    public class st_nazn_detali
    {
        public string? n_rpus { get; set; }
        public string? esr_nazn_vag { get; set; }
    }
    public class stan_detali
    {
        public string? n_rpus { get; set; }
        public int? esr_op { get; set; }
    }
    public class disl_vag_detali
    {
        public gruz_detali? gruz { get; set; }
        public int? km { get; set; }
        public string? ves_gruz { get; set; }
        public string? mnkua_opv { get; set; }
        public int kol_vag { get; set; }
        public st_otpr_detali? st_otpr { get; set; }
        public int? kod_grotp { get; set; }
        public string? mname_rv { get; set; }
        public string? esr_form { get; set; }
        public st_nazn_detali? st_nazn { get; set; }
        public string? n_dorus { get; set; }
        public string? date_pogr { get; set; }
        public string? kod_grp { get; set; }
        public string? nom_sost { get; set; }
        public string? date_op { get; set; }
        public stan_detali? stan { get; set; }
        public string? prog_cha_prib { get; set; }
        public int kod_dor { get; set; }
        public int rod_vag { get; set; }
        public string? esr_nazn { get; set; }
        public string? pr_nrp { get; set; }
    }
    public class itog_detali
    {
        public string? info { get; set; }
        public int? kol_vag { get; set; }
        public string? ves_gruz { get; set; }
    }
    public class req1892
    {
        public IList<itog_detali>? itog { get; set; }
        public IList<disl_vag_detali>? disl_vag { get; set; }
    }

    public class osn_data_detali
    {
        public string? name { get; set; }
        public string? esr_nazn_vag { get; set; }
    }
    public class sum_data_detali
    {
        public int? kol_vag_dor_n { get; set; }
    }
    public class oper_detali
    {
        public int? code_op { get; set; }
        public string? mnkua_opv_uzg { get; set; }
    }
    public class stan_disl_detali
    {
        public int? kod_esr { get; set; }
        public string? name { get; set; }
    }
    public class vag_info_detali
    {
        public string? nom_sost { get; set; }
        public string? date_op { get; set; }
        public int? kol_vag { get; set; }
        public string? esr_form { get; set; }
        public oper_detali? oper { get; set; }
        public string? esr_nazn { get; set; }
        public stan_disl_detali? stan_disl { get; set; }
        public int? nom_p { get; set; }
    }
    public class dir_info_detali
    {
        public int? kod_otd { get; set; }
        public int? kol_vag_dn { get; set; }
        public IList<vag_info_detali>? vag_info { get; set; }
    }
    public class zal_info_detali
    {
        public string? n_doru { get; set; }
        public int? kol_vag_dor { get; set; }
        public IList<dir_info_detali>? dir_info { get; set; }
        public int? kod_dor { get; set; }
    }
    public class req1091
    {
        public osn_data_detali? osn_data { get; set; }
        public IList<sum_data_detali>? sum_data { get; set; }
        public IList<zal_info_detali>? zal_info { get; set; }
    }

    public class itogs_detali
    {
        public int? kol_poezd { get; set; }
        public int? kol_vag { get; set; }
        public int? kol_vag_por_sng { get; set; }
        public int? kol_vag_gr_sng { get; set; }
        public int? kol_vag_nrp { get; set; }
        public int? forma { get; set; }
        public int? podh_prib { get; set; }
    }
    public class trains_detali {
        public int? km { get; set; }
        public string? mn_ukr { get; set; }
        public int? kol_vag { get; set; }
        public int? kol_vag_por_sng { get; set; }
        public st_otpr_detali? st_otpr { get; set; }
        public int? kol_vag_gr_sng { get; set; }
        public int? kol_vag_nrp { get; set; }
        public string? esr_form { get; set; }
        public int? forma { get; set; }
        public string? mnkua_opp { get; set; }
        public string? name_lok { get; set; }
        public int? code_op { get; set; }
        public string? n_ser { get; set; }
        public string? nom_sost { get; set; }
        public string? date_op { get; set; }
        public stan_detali? stan { get; set; }
        public string? short_name { get; set; }
        public string? esr_nazn { get; set; }
        public int? podh_prib { get; set; }
        public int? nom_p { get; set; }
    }
    public class req4373
    {
        public IList<trains_detali>? trains { get; set; }
        public IList<itogs_detali>? itogs { get; set; }
    }
    public class req7002_detali {
        public string? ves_gruz { get; set; }
        public int? kol_vag { get; set; }
        public int? kod_grotp { get; set; }
        public string? esr_form { get; set; }
        public string? esr_nazn_vag { get; set; }
        public string? nom_vag { get; set; }
        public int? code_op { get; set; }
        public string? date_otpr { get; set; }
        public string? kod_grp { get; set; }
        public string? nom_sost { get; set; }
        public string? date_op { get; set; }
        public string? etsng { get; set; }
        public int? esr_otpr { get; set; }
        public string? esr_nazn { get; set; }
        public int? nom_p { get; set; }
        public int? esr_op { get; set; }
    }
    public class req7002
    {
        public IList<req7002_detali>? Data { get; set; }
    }
    public class sluj_fraza_detali
    {
        public string? pr_marsh { get; set; }
        public string? kod_prikr { get; set; }
        public string? esr_form { get; set; }
        public string? ind_negab { get; set; }
        public string? ves_brutto { get; set; }
        public string? kod_mess { get; set; }
        public string? nom_sost { get; set; }
        public string? date_op { get; set; }
        public int? pr_spis { get; set; }
        public string? usl_dl { get; set; }
        public string? pr_zhivn { get; set; }
        public string? esr_nazn { get; set; }
        public int? esr_op { get; set; }
        public int? nom_p { get; set; }
    }
    public class info_fraza_detali
    {
        public string? pr_rol { get; set; }
        public string? pr_marsh { get; set; }
        public string? prymitka { get; set; }
        public string? ves_gruz { get; set; }
        public string? ves_tary_pogruzka { get; set; }
        public int? kol_plomb { get; set; }
        public string? kod_prikr { get; set; }
        public string? esr_nazn_vag { get; set; }
        public string? nom_vag { get; set; }
        public string? kol_zav_kont { get; set; }
        public string? esr_sd_ukr { get; set; }
        public string? kod_grp { get; set; }
        public string? por_nom { get; set; }
        public string? pr_negab { get; set; }
        public string? etsng { get; set; }
        public string? kod_adm_arc { get; set; }
        public string? kol_por_kont { get; set; }
    }
    public class req0002
    {
        public sluj_fraza_detali? sluj_fraza { get; set; }
        public IList<info_fraza_detali>? info_fraza { get; set; }
    }
    public class reqDisvag_detali
    {
        public string? ves_gruz { get; set; }
        public int? kod_grotp { get; set; }
        public string? esr_form { get; set; }
        public string? esr_nazn_vag { get; set; }
        public string? mnk_esr_op { get; set; }
        public string? nom_vag { get; set; }
        public string? mnk_esr_otpr { get; set; }
        public string? mnk_esr_nazn { get; set; }
        public int? code_op { get; set; }
        public string? date_otpr { get; set; }
        public string? kod_grp { get; set; }
        public string? date_op { get; set; }
        public string? nom_sost { get; set; }
        public string? mnk_op { get; set; }
        public string? etsng { get; set; }
        public int? esr_otpr { get; set; }
        public string? esr_nazn { get; set; }
        public string? docnom { get; set; }
        public int? esr_op { get; set; }
        public int? nom_p { get; set; }
    }
    public class reqDisvag
    {
        public IList<reqDisvag_detali>? data { get; set; }
    }

    public class fields_detali
    {
        public string? code { get; set; }
        public string? value { get; set; }
    }
    public class rows_detali
    {
        public IList<fields_detali>? fields { get; set; }
    }
    public class reqNDI
    {
        public string? TableName { get; set; }
        public IList<rows_detali>? rows { get; set; }
    }

    public class DataGIVC {

        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        public DataGIVC(ILogger<Object> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public T? GetDeserializeJSON_ApiValuesResult<T>(string jsonString)
        {
            try
            {
                T? result = System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("GetDeserializeJSON_ApiValuesResult(jsonString={0}), Exception={1})", jsonString, e));
                return default(T);
            }
        }
    }
}
