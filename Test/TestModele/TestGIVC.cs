using GIVC;
using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.TestModele
{
    public class TestGIVC
    {
        private WebClientGIVC client_givc = null;
        public TestGIVC(ILogger<Object> logger, IConfiguration config)
        {
            client_givc = new WebClientGIVC(logger, config);
        }
        /// <summary>
        /// Тест натурной ведомости
        /// </summary>
        public void Req0002()
        {
            if (client_givc != null)
            {
                req0002 res = client_givc.GetReq0002(4671, 137, 4670); // 4819-077-4670
                string path = @"D:\ГИВС\req0002_info_fraza.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                    "pr_rol;" +
                    "pr_marsh;" +
                    "prymitka;" +
                    "ves_gruz;" +
                    "ves_tary_pogruzka;" +
                    "kol_plomb;" +
                    "kod_prikr;" +
                    "esr_nazn_vag;" +
                    "nom_vag;" +
                    "kol_zav_kont;" +
                    "esr_sd_ukr;" +
                    "kod_grp;" +
                    "por_nom;" +
                    "pr_negab;" +
                    "etsng;" +
                    "kod_adm_arc;" +
                    "kol_por_kont;";
                    writer.WriteLine(str_field);
                    foreach (info_fraza_detali item in res.info_fraza.ToList())
                    {
                        string str =
                        (item.pr_rol != null ? item.pr_rol : "") + ";" +
                        (item.pr_marsh != null ? item.pr_marsh : "") + ";" +
                        (item.prymitka != null ? item.prymitka : "") + ";" +
                        (item.ves_gruz != null ? item.ves_gruz : "") + ";" +
                        (item.ves_tary_pogruzka != null ? item.ves_tary_pogruzka : "") + ";" +
                        (item.kol_plomb != null ? item.kol_plomb : "") + ";" +
                        (item.kod_prikr != null ? item.kod_prikr : "") + ";" +
                        (item.esr_nazn_vag != null ? item.esr_nazn_vag : "") + ";" +
                        (item.nom_vag != null ? item.nom_vag : "") + ";" +
                        (item.kol_zav_kont != null ? item.kol_zav_kont : "") + ";" +
                        (item.esr_sd_ukr != null ? item.esr_sd_ukr : "") + ";" +
                        (item.kod_grp != null ? item.kod_grp : "") + ";" +
                        (item.por_nom != null ? item.por_nom : "") + ";" +
                        (item.pr_negab != null ? item.pr_negab : "") + ";" +
                        (item.etsng != null ? item.etsng : "") + ";" +
                        (item.kod_adm_arc != null ? item.kod_adm_arc : "") + ";" +
                        (item.kol_por_kont != null ? item.kol_por_kont : "") + ";";
                        writer.WriteLine(str);
                    }
                }
                path = @"D:\req0002_sluj_fraza.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.WriteLine("pr_marsh;{0}", (res.sluj_fraza.pr_marsh != null ? res.sluj_fraza.pr_marsh : ""));
                    writer.WriteLine("kod_prikr;{0}", (res.sluj_fraza.kod_prikr != null ? res.sluj_fraza.kod_prikr : ""));
                    writer.WriteLine("esr_form;{0}", (res.sluj_fraza.esr_form != null ? res.sluj_fraza.esr_form : ""));
                    writer.WriteLine("ind_negab;{0}", (res.sluj_fraza.ind_negab != null ? res.sluj_fraza.ind_negab : ""));
                    writer.WriteLine("ves_brutto;{0}", (res.sluj_fraza.ves_brutto != null ? res.sluj_fraza.ves_brutto : ""));
                    writer.WriteLine("kod_mess;{0}", (res.sluj_fraza.kod_mess != null ? res.sluj_fraza.kod_mess : ""));
                    writer.WriteLine("nom_sost;{0}", (res.sluj_fraza.nom_sost != null ? res.sluj_fraza.nom_sost : ""));
                    writer.WriteLine("date_op;{0}", (res.sluj_fraza.date_op != null ? res.sluj_fraza.date_op : ""));
                    writer.WriteLine("pr_spis;{0}", (res.sluj_fraza.pr_spis != null ? res.sluj_fraza.pr_spis : ""));
                    writer.WriteLine("usl_dl;{0}", (res.sluj_fraza.usl_dl != null ? res.sluj_fraza.usl_dl : ""));
                    writer.WriteLine("pr_zhivn;{0}", (res.sluj_fraza.pr_zhivn != null ? res.sluj_fraza.pr_zhivn : ""));
                    writer.WriteLine("esr_nazn;{0}", (res.sluj_fraza.esr_nazn != null ? res.sluj_fraza.esr_nazn : ""));
                    writer.WriteLine("esr_op;{0}", (res.sluj_fraza.esr_op != null ? res.sluj_fraza.esr_op : ""));
                    writer.WriteLine("nom_p;{0}", (res.sluj_fraza.nom_p != null ? res.sluj_fraza.nom_p : ""));
                }
            }
        }
        //req1892
        /// <summary>
        /// 1892 «Універсальна форма по дислокації вагонів призначенням на задану станцію з урахуванням одержувача, вантажу, 
        /// відправника, індексу поїзда, дислокації тощо»
        /// </summary>
        public void Req1892()
        {
            if (client_givc != null)
            {
                //req1892 res = client_givc.GetReq1892(0, 467201, 7932, 7932, "0", "421034");
                req1892 res = client_givc.GetReq1892(467004, 467201, 7932, 7932, "01.01.2024", "31.12.2024");
                //req1892 res = client_givc.GetReq1892(467004, 467004, 7932, 7932);
                //req1892 res = client_givc.GetReq1892(467004, 467004);
                string path = @"D:\ГИВС\req1892.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                                "gruz_detali[etsng];" +
                                "gruz_detali[nvs];" +
                                "km;" +
                                "ves_gruz;" +
                                "mnkua_opv;" +
                               "kol_vag;" +
                               "st_otpr_detali[esr_otpr];" +
                               "st_otpr_detali[n_rpus];" +
                               "kod_grotp;" +
                               "mname_rv;" +
                               "esr_form;" +
                               "st_nazn_detali[n_rpus];" +
                               "st_nazn_detali[esr_nazn_vag];" +
                               "n_dorus;" +
                               "date_pogr;" +
                               "kod_grp;" +
                               "nom_sost;" +
                               "date_op;" +
                               "stan_detali[n_rpus];" +
                               "stan_detali[esr_op];" +
                               "prog_cha_prib;" +
                               "kod_dor;" +
                               "rod_vag;" +
                               "esr_nazn;" +
                               "pr_nrp;";
                    writer.WriteLine(str_field);


                    foreach (disl_vag_detali disl in res.disl_vag.ToList())
                    {
                        string str =
                            (disl.gruz != null && disl.gruz.etsng != null ? disl.gruz.etsng : "") + ";" +
                            (disl.gruz != null && disl.gruz.nvs != null ? disl.gruz.nvs : "") + ";" +
                            (disl.km != null ? disl.km : "") + ";" +
                            (disl.ves_gruz != null ? disl.ves_gruz : "") + ";" +
                            (disl.mnkua_opv != null ? disl.mnkua_opv : "") + ";" +
                            (disl.kol_vag != null ? disl.kol_vag : "") + ";" +
                            (disl.st_otpr != null && disl.st_otpr.esr_otpr != null ? disl.st_otpr.esr_otpr : "") + ";" +
                            (disl.st_otpr != null && disl.st_otpr.n_rpus != null ? disl.st_otpr.n_rpus : "") + ";" +
                            (disl.kod_grotp != null ? disl.kod_grotp : "") + ";" +
                            (disl.mname_rv != null ? disl.mname_rv : "") + ";" +
                            (disl.esr_form != null ? disl.esr_form : "") + ";" +
                            (disl.st_nazn != null && disl.st_nazn.n_rpus != null ? disl.st_nazn.n_rpus : "") + ";" +
                            (disl.st_nazn != null && disl.st_nazn.esr_nazn_vag != null ? disl.st_nazn.esr_nazn_vag : "") + ";" +
                            (disl.n_dorus != null ? disl.n_dorus : "") + ";" +
                            (disl.date_pogr != null ? disl.date_pogr : "") + ";" +
                            (disl.kod_grp != null ? disl.kod_grp : "") + ";" +
                            (disl.nom_sost != null ? disl.nom_sost : "") + ";" +
                            (disl.date_op != null ? disl.date_op : "") + ";" +
                            (disl.stan != null && disl.stan.n_rpus != null ? disl.stan.n_rpus : "") + ";" +
                            (disl.stan != null && disl.stan.esr_op != null ? disl.stan.esr_op : "") + ";" +
                            (disl.prog_cha_prib != null ? disl.prog_cha_prib : "") + ";" +
                            (disl.kod_dor != null ? disl.kod_dor : "") + ";" +
                            (disl.rod_vag != null ? disl.rod_vag : "") + ";" +
                            (disl.esr_nazn != null ? disl.esr_nazn : "") + ";" +
                            (disl.pr_nrp != null ? disl.pr_nrp : "") + ";";
                        writer.WriteLine(str);
                    }
                }
            }
        }
        //req1091
        /// <summary>
        /// 1091 «Підхід порожніх вагонів до станції»
        /// </summary>
        public void Req1091()
        {
            if (client_givc != null)
            {
                req1091 res = client_givc.GetReq1091(0, 7932, 7932, 467004);
                string path = @"D:\ГИВС\req1091.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                    "n_doru;" +
                    "kol_vag_dor;" +
                    "kod_otd;" +
                    "kol_vag_dn;" +
                    "nom_sost;" +
                    "date_op;" +
                    "kol_vag;" +
                    "esr_form;" +
                    "oper.code_op;" +
                    "oper.mnkua_opv_uzg;" +
                    "esr_nazn;" +
                    "stan_disl.kod_esr;" +
                    "stan_disl.name;" +
                    "nom_p;" +
                    "kod_dor;";
                    writer.WriteLine(str_field);
                    //
                    foreach (var item in res.zal_info)
                    {
                        //item.n_doru;
                        //item.kol_vag_dor;
                        foreach (var item_dir in item.dir_info)
                        {
                            //item_dir.kod_otd;
                            //item_dir.kol_vag_dn;
                            foreach (var item_vag in item_dir.vag_info)
                            {
                                //item.n_doru;
                                //item.kol_vag_dor;
                                //item_dir.kod_otd;
                                //item_dir.kol_vag_dn;
                                //item_vag.nom_sost;
                                //item_vag.date_op;
                                //item_vag.kol_vag;
                                //item_vag.esr_form;
                                //item_vag.oper.code_op;
                                //item_vag.oper.mnkua_opv_uzg;
                                //item_vag.esr_nazn;
                                //item_vag.stan_disl.kod_esr;
                                //item_vag.stan_disl.name;
                                //item_vag.nom_p;
                                //item.kod_dor;
                                string str =
                                    (item.n_doru != null ? item.n_doru : "") + ";" +
                                    (item.kol_vag_dor != null ? item.kol_vag_dor : "") + ";" +
                                    (item_dir.kod_otd != null ? item_dir.kod_otd : "") + ";" +
                                    (item_dir.kol_vag_dn != null ? item_dir.kol_vag_dn : "") + ";" +
                                    (item_vag.nom_sost != null ? item_vag.nom_sost : "") + ";" +
                                    (item_vag.date_op != null ? item_vag.date_op : "") + ";" +
                                    (item_vag.kol_vag != null ? item_vag.kol_vag : "") + ";" +
                                    (item_vag.esr_form != null ? item_vag.esr_form : "") + ";" +
                                    (item_vag.oper != null && item_vag.oper.code_op != null ? item_vag.oper.code_op : "") + ";" +
                                    (item_vag.oper != null && item_vag.oper.mnkua_opv_uzg != null ? item_vag.oper.mnkua_opv_uzg : "") + ";" +
                                    (item_vag.esr_nazn != null ? item_vag.esr_nazn : "") + ";" +
                                    (item_vag.stan_disl != null && item_vag.stan_disl.kod_esr != null ? item_vag.stan_disl.kod_esr : "") + ";" +
                                    (item_vag.stan_disl != null && item_vag.stan_disl.name != null ? item_vag.stan_disl.name : "") + ";" +
                                    (item_vag.nom_p != null ? item_vag.nom_p : "") + ";" +
                                    (item.kod_dor != null ? item.kod_dor : "") + ";";
                                writer.WriteLine(str);
                            }
                        }
                        //item.kod_dor

                    }
                }
            }
        }
        //req4373
        /// <summary>
        /// 7002 «Підхід вагонів під вивантаження»
        /// </summary>
        public void Req4373()
        {
            if (client_givc != null)
            {
                req4373 res = client_givc.GetReq4373(467004, 0);
                string path = @"D:\ГИВС\req4373.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                    "km;" +
                    "mn_ukr;" +
                    "kol_vag;" +
                    "kol_vag_por_sng;" +
                    "st_otpr.esr_otpr;" +
                    "st_otpr.n_rpus;" +
                    "kol_vag_gr_sng;" +
                    "kol_vag_nrp;" +
                    "esr_form;" +
                    "forma;" +
                    "mnkua_opp;" +
                    "name_lok;" +
                    "code_op;" +
                    "n_ser;" +
                    "nom_sost;" +
                    "date_op;" +
                    "stan.esr_op;" +
                    "stan.n_rpus;" +
                    "short_name;" +
                    "esr_nazn;" +
                    "podh_prib;" +
                    "nom_p;";
                    writer.WriteLine(str_field);
                    //
                    foreach (var item in res.trains)
                    {
                        string str =
                        (item.km != null ? item.km : "") + ";" +
                        (item.mn_ukr != null ? item.mn_ukr : "") + ";" +
                        (item.kol_vag != null ? item.kol_vag : "") + ";" +
                        (item.kol_vag_por_sng != null ? item.kol_vag_por_sng : "") + ";" +
                        (item.st_otpr != null && item.st_otpr.esr_otpr != null ? item.st_otpr.esr_otpr : "") + ";" +
                        (item.st_otpr != null && item.st_otpr.n_rpus != null ? item.st_otpr.n_rpus : "") + ";" +
                        (item.kol_vag_gr_sng != null ? item.kol_vag_gr_sng : "") + ";" +
                        (item.kol_vag_nrp != null ? item.kol_vag_nrp : "") + ";" +
                        (item.esr_form != null ? item.esr_form : "") + ";" +
                        (item.forma != null ? item.forma : "") + ";" +
                        (item.mnkua_opp != null ? item.mnkua_opp : "") + ";" +
                        (item.name_lok != null ? item.name_lok : "") + ";" +
                        (item.code_op != null ? item.code_op : "") + ";" +
                        (item.n_ser != null ? item.n_ser : "") + ";" +
                        (item.nom_sost != null ? item.nom_sost : "") + ";" +
                        (item.date_op != null ? item.date_op : "") + ";" +
                        (item.stan != null && item.stan.esr_op != null ? item.stan.esr_op : "") + ";" +
                        (item.stan != null && item.stan.n_rpus != null ? item.stan.n_rpus : "") + ";" +
                        (item.short_name != null ? item.short_name : "") + ";" +
                        (item.esr_nazn != null ? item.esr_nazn : "") + ";" +
                        (item.podh_prib != null ? item.podh_prib : "") + ";" +
                        (item.nom_p != null ? item.nom_p : "") + ";";
                        writer.WriteLine(str);
                    }
                }
            }
        }
        //req7002
        public void Req7002()
        {
            if (client_givc != null)
            {
                req7002 res = client_givc.GetReq7002("24432974", 467004, 467201, 7932, 7932);
                string path = @"D:\ГИВС\req7002.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                    "ves_gruz;" +
                    "kol_vag;" +
                    "kod_grotp;" +
                    "esr_form;" +
                    "esr_nazn_vag;" +
                    "nom_vag;" +
                    "code_op;" +
                    "date_otpr;" +
                    "kod_grp;" +
                    "nom_sost;" +
                    "date_op;" +
                    "etsng;" +
                    "esr_otpr;" +
                    "esr_nazn;" +
                    "nom_p;" +
                    "esr_op;";
                    writer.WriteLine(str_field);
                    //
                    foreach (var item in res.Data)
                    {
                        string str =
                        (item.ves_gruz != null ? item.ves_gruz : "") + ";" +
                        (item.kol_vag != null ? item.kol_vag : "") + ";" +
                        (item.kod_grotp != null ? item.kod_grotp : "") + ";" +
                        (item.esr_form != null ? item.esr_form : "") + ";" +
                        (item.esr_nazn_vag != null ? item.esr_nazn_vag : "") + ";" +
                        (item.nom_vag != null ? item.nom_vag : "") + ";" +
                        (item.code_op != null ? item.code_op : "") + ";" +
                        (item.date_otpr != null ? item.date_otpr : "") + ";" +
                        (item.kod_grp != null ? item.kod_grp : "") + ";" +
                        (item.nom_sost != null ? item.nom_sost : "") + ";" +
                        (item.date_op != null ? item.date_op : "") + ";" +
                        (item.etsng != null ? item.etsng : "") + ";" +
                        (item.esr_otpr != null ? item.esr_otpr : "") + ";" +
                        (item.esr_nazn != null ? item.esr_nazn : "") + ";" +
                        (item.nom_p != null ? item.nom_p : "") + ";" +
                        (item.esr_op != null ? item.esr_op : "") + ";";
                        writer.WriteLine(str);
                    }
                }
            }
        }
        public void reqDisvag()
        {
            if (client_givc != null)
            {
                //reqDisvag res = client_givc.GetReqDisvag(454606, 8887, 467004, 7932, 161113);
                reqDisvag res = client_givc.GetReqDisvag("24432974", 7932);
                string path = @"D:\ГИВС\reqDisvag.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                    "ves_gruz;" +
                    "kod_grotp;" +
                    "esr_form;" +
                    "esr_nazn_vag;" +
                    "mnk_esr_op;" +
                    "nom_vag;" +
                    "mnk_esr_otpr;" +
                    "mnk_esr_nazn;" +
                    "code_op;" +
                    "date_otpr;" +
                    "kod_grp;" +
                    "date_op;" +
                    "nom_sost;" +
                    "mnk_op;" +
                    "etsng;" +
                    "esr_otpr;" +
                    "esr_nazn;" +
                    "docnom;" +
                    "esr_op;" +
                    "nom_p;";
                    writer.WriteLine(str_field);
                    //
                    foreach (var item in res.data)
                    {
                        string str =
                        (item.ves_gruz != null ? item.ves_gruz : "") + ";" +
                        (item.kod_grotp != null ? item.kod_grotp : "") + ";" +
                        (item.esr_form != null ? item.esr_form : "") + ";" +
                        (item.esr_nazn_vag != null ? item.esr_nazn_vag : "") + ";" +
                        (item.mnk_esr_op != null ? item.mnk_esr_op : "") + ";" +
                        (item.nom_vag != null ? item.nom_vag : "") + ";" +
                        (item.mnk_esr_otpr != null ? item.mnk_esr_otpr : "") + ";" +
                        (item.mnk_esr_nazn != null ? item.mnk_esr_nazn : "") + ";" +
                        (item.code_op != null ? item.code_op : "") + ";" +
                        (item.date_otpr != null ? item.date_otpr : "") + ";" +
                        (item.kod_grp != null ? item.kod_grp : "") + ";" +
                        (item.date_op != null ? item.date_op : "") + ";" +
                        (item.nom_sost != null ? item.nom_sost : "") + ";" +
                        (item.mnk_op != null ? item.mnk_op : "") + ";" +
                        (item.etsng != null ? item.etsng : "") + ";" +
                        (item.esr_otpr != null ? item.esr_otpr : "") + ";" +
                        (item.esr_nazn != null ? item.esr_nazn : "") + ";" +
                        (item.docnom != null ? item.docnom : "") + ";" +
                        (item.esr_op != null ? item.esr_op : "") + ";" +
                        (item.nom_p != null ? item.nom_p : "") + ";";
                        writer.WriteLine(str);
                    }
                }
            }
        }
        public void reqNDI()
        {
            if (client_givc != null)
            {
                reqNDI res = client_givc.GetReqNDI(1);
                string path = @"D:\ГИВС\Довідник_Операції.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    string str_field =
                    "KodOp;" +
                    "PrOP;" +
                    "NameOp;" +
                    "MnkOp;";
                    writer.WriteLine(str_field);
                    //
                    foreach (var item in res.rows)
                    {
                        string str =
                        (item.fields != null && item.fields.Count > 0 && item.fields[0].value != null ? item.fields[0].value : "") + ";" +
                        (item.fields != null && item.fields.Count > 0 && item.fields[1].value != null ? item.fields[1].value : "") + ";" +
                        (item.fields != null && item.fields.Count > 0 && item.fields[2].value != null ? item.fields[2].value : "") + ";" +
                        (item.fields != null && item.fields.Count > 0 && item.fields[3].value != null ? item.fields[3].value : "") + ";";
                        writer.WriteLine(str);
                    }
                }
            }
        }

    }
}
