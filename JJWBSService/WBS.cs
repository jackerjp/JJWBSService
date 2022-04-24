using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JJWBSService.Modes;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace JJWBSService
{

    public class SqlDataBase
    {
        private const int MaxPool = 512;//最大连接数
        private const int MinPool = 5;//最小连接数
        private const bool Asyn_Process = true;//设置异步访问数据库
                                               //在单个连接上得到和管理多个、仅向前引用和只读的结果集(ADO.NET2.0)
        private const bool Mars = true;
        private const int Conn_Timeout = 10000;//设置连接等待时间
        private const int Conn_Lifetime = 1500;//设置连接的生命周期
        private SqlConnection con = null;//连接对象
        private SqlTransaction dbTran = null;
        public Dictionary<string, object> paramList = new Dictionary<string, object>();

        public SqlDataBase()
        {
            //server=47.101.200.39;database=JJPowerPMDBTest;uid=sa;pwd=Power3506
            string sConnectionString = @"server=47.101.200.39;database=JJPowerPMDBTest;uid=sa;pwd=Power3506;"
                                     + "Max Pool Size=" + MaxPool + ";"
                                     + "Min Pool Size=" + MinPool + ";"
                                     + "Connect Timeout=" + Conn_Timeout + ";"
                                     + "Connection Lifetime=" + Conn_Lifetime + ";"
                                     + "Asynchronous Processing=" + Asyn_Process + ";";
            con = new SqlConnection(sConnectionString);
        }

        public SqlDataBase(string sLJCS)
        {
            string sConnectionString = sLJCS + ";";
            sConnectionString += @"Max Pool Size=" + MaxPool + ";"
                               + "Min Pool Size=" + MinPool + ";"
                               + "Connect Timeout=" + Conn_Timeout + ";"
                               + "Connection Lifetime=" + Conn_Lifetime + ";"
                               + "Asynchronous Processing=" + Asyn_Process + ";";
            con = new SqlConnection(sConnectionString);
        }

        public void beginTran()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            dbTran = con.BeginTransaction();
        }

        public void commitTran()
        {
            try
            {
                dbTran.Commit();
            }
            catch
            {
                dbTran.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        public void rollback()
        {
            dbTran.Rollback();
            con.Close();
        }

        public DataSet getDataSet(string sSQL)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sSQL, con);
            cmd.Parameters.Clear();
            foreach (KeyValuePair<string, object> dic in paramList)
                cmd.Parameters.AddWithValue(dic.Key, dic.Value);
            if (dbTran != null)
                cmd.Transaction = dbTran;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            clearParam();
            if (dbTran == null)
                if (dbTran == null)
                    con.Close();
            return ds;
        }

        public void doSQL(string sSQL)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand(sSQL, con);
            cmd.Parameters.Clear();
            foreach (KeyValuePair<string, object> dic in paramList)
                cmd.Parameters.AddWithValue(dic.Key, dic.Value);
            if (dbTran != null)
                cmd.Transaction = dbTran;
            cmd.ExecuteNonQuery();
            clearParam();
            if (dbTran == null)
                con.Close();
        }

        public void addParam(string param, object value)
        {
            paramList.Add(param, value);
        }

        public void clearParam()
        {
            paramList.Clear();
        }
    }
    public class MySQLDataBase
    {
        private const int MaxPool = 300;//最大连接数
        private const int MinPool = 5;//最小连接数
        private const bool Asyn_Process = true;//设置异步访问数据库
                                               //在单个连接上得到和管理多个、仅向前引用和只读的结果集(ADO.NET2.0)
        private const bool Mars = true;
        private const int Conn_Timeout = 1000;//设置连接等待时间
        private const int Conn_Lifetime = 1500;//设置连接的生命周期
        private MySqlConnection con = null;//连接对象
        private MySqlTransaction dbTran = null;
        public Dictionary<string, object> paramList = new Dictionary<string, object>();

        public MySQLDataBase()
        {
            //server=47.101.200.39;database=JJPowerPMDBTest;uid=sa;pwd=Power3506
            string sConnectionString = @"server=218.94.73.106;port=8306;user=JJSDBASE;password=JJSDBASE; database=jybim;";
            con = new MySqlConnection(sConnectionString);
        }

        public MySQLDataBase(string sLJCS)
        {
            string sConnectionString = sLJCS + ";";
            con = new MySqlConnection(sConnectionString);
        }

        public void beginTran()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            dbTran = con.BeginTransaction();
        }

        public void commitTran()
        {
            try
            {
                dbTran.Commit();
            }
            catch
            {
                dbTran.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        public void rollback()
        {
            dbTran.Rollback();
            con.Close();
        }

        public DataSet getDataSet(string sSQL)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            MySqlCommand cmd = new MySqlCommand(sSQL, con);
            cmd.Parameters.Clear();
            foreach (KeyValuePair<string, object> dic in paramList)
                cmd.Parameters.AddWithValue(dic.Key, dic.Value);
            if (dbTran != null)
                cmd.Transaction = dbTran;
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            clearParam();
            if (dbTran == null)
                con.Close();
            return ds;
        }

        public void doSQL(string sSQL)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            MySqlCommand cmd = new MySqlCommand(sSQL, con);
            cmd.Parameters.Clear();
            foreach (KeyValuePair<string, object> dic in paramList)
                cmd.Parameters.AddWithValue(dic.Key, dic.Value);
            if (dbTran != null)
                cmd.Transaction = dbTran;
            cmd.ExecuteNonQuery();
            clearParam();
            if (dbTran == null)
                con.Close();
        }

        public void addParam(string param, object value)
        {
            paramList.Add(param, value);
        }

        public void clearParam()
        {
            paramList.Clear();
        }
    }
    public  class WBS
    {
        //static string A1parent_wbs_guid = "5f66a65e-80e1-4e16-8152-d5e3bfe4d163";
        //static string A2parent_wbs_guid = "eb8d2759-7d39-4559-8316-77c204bb20cb";
        //static string A1NowPlan_guid = "e223174f-c611-d125-a275-b3ca7d70eb87";
        //static string A2NowPlan_guid = "c1e9edbb-358b-3585-aefd-a270549628af";
        //static string A1BeforePlan_guid = "c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74";
        //static string A2BeforePlan_guid = "8fafab0e-da05-607e-20d7-0faa24076e90";
        //static string NowProj_guid = "8ba0045f-ba50-4b00-898d-50337ff12182";
        //static string BeforeProj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";

        static string A1parent_wbs_guid = "9a157829-a7da-4a27-8068-ec64c62a346a";
        static string A2parent_wbs_guid = "2a080847-1375-4bcc-bb32-c5c51521d8e9";
        static string A1NowPlan_guid = "a2458514-48eb-5111-5e1b-599a7d56c0a5";
        static string A2NowPlan_guid = "3d82fc97-0ca9-3409-9e03-b11e565f5836";
        static string A1BeforePlan_guid = "e223174f-c611-d125-a275-b3ca7d70eb87";
        static string A2BeforePlan_guid = "c1e9edbb-358b-3585-aefd-a270549628af";
        static string NowProj_guid = "6fddf002-fc9d-494c-b615-1827796b27be";
        static string BeforeProj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";

        static string InsertNowProjWbs = @"insert into pln_projwbs (wbs_id,wbs_guid,proj_id,proj_guid,obs_id,obs_guid,seq_num,est_wt,complete_pct,proj_node_flag,sum_data_flag,status_code,wbs_short_name,wbs_name,phase_id,parent_wbs_id,parent_wbs_guid,ev_user_pct,ev_etc_user_value,orig_cost,user_cost1,indep_remain_total_cost,
                                            ann_dscnt_rate_pct,dscnt_period_type,indep_remain_work_qty,anticip_start_date,anticip_end_date,ev_compute_type,ev_etc_compute_type,guid,tmpl_guid,p3ec_wbs_id,p3ec_parentwbs_id,p3ec_flag,treelevel,haschild,discolor,target_start_date,target_end_date,expect_start_date,expect_end_date,
                                            act_start_date,act_end_date,SysOrNot,allowModifyTaskOrNot,complete_pct_type,target_drtn_hr_cnt,remain_drtn_hr_cnt,update_date,start_date,end_date,LongCode,plan_id,plan_guid,est_wt_pct,require_start_date,require_end_date,main_wbs_guid,bcws_cost,bcwp_cost,
                                            acwp_cost,budget_cost,reghumanid,reghumanname,rsrc_guid,rsrc_name,responsibleid,responsiblename,update_user,create_date,create_user,delete_session_id,delete_date,target_complete_pct,period_target_complete_pct,
                                            restart_date,reend_date,plan_pct,wbs_guid_before,dept_name,dept_guid,remark,plan_hour,act_hour)
                                            select wbs_id,wbs_guid,proj_id,proj_guid,obs_id,obs_guid,seq_num,est_wt,complete_pct,proj_node_flag,sum_data_flag,status_code,wbs_short_name,wbs_name,phase_id,parent_wbs_id,parent_wbs_guid,ev_user_pct,ev_etc_user_value,orig_cost,user_cost1,indep_remain_total_cost,
                                            ann_dscnt_rate_pct,dscnt_period_type,indep_remain_work_qty,anticip_start_date,anticip_end_date,ev_compute_type,ev_etc_compute_type,guid,tmpl_guid,p3ec_wbs_id,p3ec_parentwbs_id,p3ec_flag,treelevel,haschild,discolor,target_start_date,target_end_date,expect_start_date,expect_end_date,
                                            act_start_date,act_end_date,SysOrNot,allowModifyTaskOrNot,complete_pct_type,target_drtn_hr_cnt,remain_drtn_hr_cnt,update_date,start_date,end_date,LongCode,plan_id,plan_guid,est_wt_pct,require_start_date,require_end_date,main_wbs_guid,bcws_cost,bcwp_cost,
                                            acwp_cost,budget_cost,reghumanid,reghumanname,rsrc_guid,rsrc_name,responsibleid,responsiblename,update_user,create_date,create_user,delete_session_id,delete_date,target_complete_pct,period_target_complete_pct,
                                            restart_date,reend_date,plan_pct,wbs_guid_before,dept_name,dept_guid,remark,plan_hour,act_hour from pln_tempprojwbs where plan_guid='"+ A1NowPlan_guid + "'";

        static string InsertNowTask = @"insert into PLN_task(task_guid,proj_id,proj_guid,wbs_id,wbs_guid,clndr_id,clndr_guid,plan_id,plan_guid,
                                        seq_num,parent_task_id,parent_guid,est_wt,complete_pct,rev_fdbk_flag,lock_plan_flag,auto_compute_act_flag,complete_pct_type,
                                        task_type,duration_type,review_type,status_code,task_code,task_name,rsrc_name,rsrc_id,rsrc_guid,total_float_hr_cnt,free_float_hr_cnt,
                                        target_drtn_hr_cnt,act_drtn_hr_cnt,remain_drtn_hr_cnt,act_work_qty,remain_work_qty,target_work_qty,target_equip_qty,act_equip_qty,remain_equip_qty,
                                        cstr_type,cstr_date,late_start_date,late_end_date,early_start_date,early_end_date,restart_date,reend_date,review_end_date,rem_late_start_date,rem_late_end_date,
                                        priority_type,guid,tmpl_guid,cstr_date2,cstr_type2,act_this_per_work_qty,act_this_per_equip_qty,driving_path_flag,float_path,float_path_order,suspend_date,resume_date,
                                        external_early_start_date,external_late_end_date,update_date,update_user,create_date,create_user_sid,create_user,delete_session_id,delete_date,act_start_date,act_end_date,
                                        expect_start_date,expect_end_date,target_start_date,target_end_date,SysOrNot,rec_type,temp_id,p3ec_flag,p3ec_task_id,data_date,start_date,end_date,plan_task_id_befor,plan_task_guid_before,
                                        memo,module_type,plan_actvcode_guid,plan_actvcode_id,parent_task_plan_guid,est_wt_pct,curve_guid,feedback_pct_type,baseline_start_date,baseline_end_date,baseline2_start_date,baseline2_end_date,
                                        baseline3_start_date,baseline3_end_date,progress_rsrc_unit_name,progress_rsrc_unit_price,target_progress_rsrc_cost,act_progress_rsrc_cost,remain_progress_rsrc_cost,target_progress_rsrc_curv,
                                        act_progress_rsrc_curv,remain_progress_rsrc_curv,target_progress_rsrc_guid,target_progress_rsrc_code,target_progress_rsrc_qty,act_progress_rsrc_qty,remain_progress_rsrc_qty,videoUrl,bcws_cost,bcwp_cost,acwp_cost,
                                        istopbreakdown,budget_cost,attach_flag,UPDATE_SESSION_ID,target_complete_pct,period_target_complete_pct,important_node_flag,plan_pct,feedback_data_date,dept_name,dept_guid,remark,plan_hour,act_hour,TempTask_code)
                                        select task_guid,proj_id,proj_guid,wbs_id,wbs_guid,clndr_id,clndr_guid,plan_id,plan_guid,
                                        seq_num,parent_task_id,parent_guid,est_wt,complete_pct,rev_fdbk_flag,lock_plan_flag,auto_compute_act_flag,complete_pct_type,
                                        task_type,duration_type,review_type,status_code,task_code,task_name,rsrc_name,rsrc_id,rsrc_guid,total_float_hr_cnt,free_float_hr_cnt,
                                        target_drtn_hr_cnt,act_drtn_hr_cnt,remain_drtn_hr_cnt,act_work_qty,remain_work_qty,target_work_qty,target_equip_qty,act_equip_qty,remain_equip_qty,
                                        cstr_type,cstr_date,late_start_date,late_end_date,early_start_date,early_end_date,restart_date,reend_date,review_end_date,rem_late_start_date,rem_late_end_date,
                                        priority_type,guid,tmpl_guid,cstr_date2,cstr_type2,act_this_per_work_qty,act_this_per_equip_qty,driving_path_flag,float_path,float_path_order,suspend_date,resume_date,
                                        external_early_start_date,external_late_end_date,update_date,update_user,create_date,create_user_sid,create_user,delete_session_id,delete_date,act_start_date,act_end_date,
                                        expect_start_date,expect_end_date,target_start_date,target_end_date,SysOrNot,rec_type,temp_id,p3ec_flag,p3ec_task_id,data_date,start_date,end_date,plan_task_id_befor,plan_task_guid_before,
                                        memo,module_type,plan_actvcode_guid,plan_actvcode_id,parent_task_plan_guid,est_wt_pct,curve_guid,feedback_pct_type,baseline_start_date,baseline_end_date,baseline2_start_date,baseline2_end_date,
                                        baseline3_start_date,baseline3_end_date,progress_rsrc_unit_name,progress_rsrc_unit_price,target_progress_rsrc_cost,act_progress_rsrc_cost,remain_progress_rsrc_cost,target_progress_rsrc_curv,
                                        act_progress_rsrc_curv,remain_progress_rsrc_curv,target_progress_rsrc_guid,target_progress_rsrc_code,target_progress_rsrc_qty,act_progress_rsrc_qty,remain_progress_rsrc_qty,videoUrl,bcws_cost,bcwp_cost,acwp_cost,
                                        istopbreakdown,budget_cost,attach_flag,UPDATE_SESSION_ID,target_complete_pct,period_target_complete_pct,important_node_flag,plan_pct,feedback_data_date,dept_name,dept_guid,remark,plan_hour,act_hour,TempTask_code from PLN_tepmtask
                                        where plan_guid='"+ A1NowPlan_guid + "'";

        //static int Proj_id = 8;
        //static int A1Plan_id = 1030;
        //static int A2Plan_id = 1031;

        static int Proj_id = 10;
        static int A1Plan_id = 1037;
        static int A2Plan_id = 1038;

        static List<PLN_TempPROJWBS> lN_TempPROJWBs = new List<PLN_TempPROJWBS>();//wbs临时表
        static List<PLN_Tepmtask> lN_TempTasks = new List<PLN_Tepmtask>();//作业临时表

        static List<PLN_PROJWBS> lN_PROJWBs = new List<PLN_PROJWBS>();
        static List<PLN_task> lN_Tasks = new List<PLN_task>();
        static List<PLN_taskproc> lN_Taskprocs = new List<PLN_taskproc>();
        static List<plN_Temptaskproc> lN_TempTaskprocs = new List<plN_Temptaskproc>();
        static List<PS_PLN_TaskProc_Sub> In_TaskProc_Sub = new List<PS_PLN_TaskProc_Sub>();



        static Dictionary<string, Guid> Gx = new Dictionary<string, Guid>();
        static Dictionary<string, DataRow> GxDx = new Dictionary<string, DataRow>();

        static Dictionary<string, Guid> Gj = new Dictionary<string, Guid>();
        static Dictionary<string, DataRow> GjDx = new Dictionary<string, DataRow>();

        static Dictionary<string, Guid> Zy = new Dictionary<string, Guid>();
        static Dictionary<string, DataRow> ZyDx = new Dictionary<string, DataRow>();

        static Dictionary<string, Guid> Oteher = new Dictionary<string, Guid>();
        static Dictionary<string, DataRow> OteherDx = new Dictionary<string, DataRow>();

        static Dictionary<string, Guid> BcOteher = new Dictionary<string, Guid>();
        static Dictionary<string, DataRow> BcOteherDx = new Dictionary<string, DataRow>();

        static Dictionary<string, string> dic = new Dictionary<string, string>();

        public static void updaets()
        {
            DataTable dataTable = DBService.JJ.FromSql("select sum(t.money_f) from T_MIDMEASURE t where t.issue in(select ISSUE from v_all_MEASUREAUDIT where STATE=1) and   t.htd='001001001'").ToDataTable();
            DataTable NPS_boq = DBService.Context.FromSql("select * from NPS_boq ").ToDataTable();
            List<NPS_BOQ> InsertDt = DBService.Context.FromSql("select * from NPS_boq ").ToList<NPS_BOQ>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                NPS_BOQ d = InsertDt.Find(X => X.S_ISBN == dataTable.Rows[i]["WBSCODE"].ToString());
                if (d != null)
                {
                    
                }
            }
        }

        public  static  void GetWBS()
        {
            //获取表信息

            DataTable WBS = DBService.JJ.FromSql("select * from T_WBS").ToDataTable();

            //筛选出所有WBS层级
            //筛选出所有构件工序层级
            ArrayList YcL = new ArrayList();

            DataRow[] GxList = WBS.Select(" N_JDLX=8");
            foreach (DataRow item in GxList)
            {
                if (item["S_ISBN"].ToString().Substring(0,9)!="001001002")
                {
                    continue;
                }
                Guid guid = Guid.NewGuid();
                Gx.Add(item["S_ISBN"].ToString(),guid);
                GxDx.Add(item["S_ISBN"].ToString(),item);
            }
         
            foreach (string item in Gx.Keys)
            {
                DataRow[] dr = WBS.Select(" S_ISBN='"+GxDx[item]["S_FISBN"] +"'");
                if (dr.Length>0)
                {
                    Guid guid = Guid.NewGuid();
                    foreach (DataRow child in dr)
                    {
                        if (dr[0]["s_isbn"].ToString()== "001001002002001001011004")
                        {
                            string aaaaaa= "";
                        }
                        if (!Gj.Keys.Contains(dr[0]["S_ISBN"].ToString()))
                        {
                            Gj.Add(child["S_ISBN"].ToString(), guid);
                            GjDx.Add(child["S_ISBN"].ToString(), child);
                        }
                    }
                   
                   
                }
            }

            //筛选出重复的
            foreach (string  item in Gj.Keys)
            {
                if (Gj.Keys.Contains(GjDx[item]["S_FISBN"]))
                {
                    ////存在与自己父亲同级，父亲的向下移动一级
                    //DataRow dr = GjDx[GjDx[item]["S_FISBN"].ToString()];
                    //DataRow newr = WBS.NewRow();
                    //newr.ItemArray = dr.ItemArray;
                    //newr["S_ISBN"] = dr["S_ISBN"] + "-";
                    //dr["S_FISBN"]= dr["S_ISBN"] + "-";
                    //WBS.Rows.Add(newr);
                    ////Gj.Remove(GjDx[item]["S_FISBN"].ToString());
                    ////重新加入
                    //GjDx[item]["S_ISBN"]= dr["S_ISBN"] + "-";

                }
            }

            foreach (string item in Gj.Keys)
            {
                DataRow[] dr = WBS.Select(" S_ISBN='" + GjDx[item]["S_FISBN"] + "'"); 
                if (dr.Length > 0)
                {
                    foreach (DataRow child in dr)
                    {
                        if (child["S_ISBN"].ToString() == "001001002002001001007" || child["S_ISBN"].ToString() == "001001002002001001002" || child["S_ISBN"].ToString()=="001001002002001003001"
                            || child["S_ISBN"].ToString()=="001001002002001003002001")
                        {
                           // continue;
                        }
                        DataRow newdr = WBS.NewRow();
                        if (!Zy.Keys.Contains(child["S_ISBN"].ToString()))
                        {
                            Guid guid = Guid.NewGuid();
                            Zy.Add(child["S_ISBN"].ToString(), guid);
                            ZyDx.Add(child["S_ISBN"].ToString(), child);
                        }
                    }
                    
                   
                }
            }
            ArrayList arrayList = new ArrayList();
            ArrayList aa = new ArrayList();
            ArrayList yclZy = new ArrayList();
            foreach (string item in Zy.Keys)
            {
               
                if (ZyDx[item]["S_NAME"].ToString() == "Y1~Y30")
                {
                    string aaaa = "";
                }
                if (ZyDx[item]["S_ISBN"].ToString() == "001001002002001001002")
                {
                    string aaaaaa = "";
                }
                if (Zy.Keys.Contains(ZyDx[item]["S_FISBN"]))
                {
                    if (yclZy.Contains(ZyDx[item]["S_FISBN"]))
                    {
                        continue;
                    }
                    arrayList.Add(ZyDx[item]["S_FISBN"].ToString());

                    DataRow dr = ZyDx[ZyDx[item]["S_FISBN"].ToString()];
                    DataRow newinstdr = WBS.NewRow();
                    newinstdr.ItemArray = dr.ItemArray;
                    WBS.Rows.Add(newinstdr);
                    DataRow newr = WBS.NewRow();
                    string key = dr["S_ISBN"].ToString();
                    newr["S_FISBN"] = newinstdr["S_ISBN"];
                    newr["S_NAME"] = dr["S_NAME"];
                    dr["S_FISBN"] = dr["S_ISBN"];
                    dr["S_ISBN"] = dr["S_ISBN"] + "-";
                    aa.Add(dr["S_ISBN"]);
                    ZyDx.Add(dr["S_ISBN"] + "", dr);
                    dic.Add(newr["S_FISBN"].ToString(), dr["S_ISBN"].ToString());
                    //循环所有构件对象
               
                    yclZy.Add(ZyDx[item]["S_FISBN"]);

                    //存在与自己父亲同级，父亲的向下移动一级



                }
            }
            foreach (string gjk in GjDx.Keys)
            {
                if (dic.Keys.Contains(GjDx[gjk]["S_FISBN"]))
                {
                    GjDx[gjk]["S_FISBN"] = dic[GjDx[gjk]["S_FISBN"].ToString()];
                }
            }
            foreach (string item in arrayList)
            {
                Zy.Remove(item);
                ZyDx.Remove(item);
            }
            foreach (string  item in aa)
            {
                Zy.Add(item, Guid.NewGuid());
            }
            //处理第一第二层级
            //处理第一第二层级后的层级

            DataRow A2 = WBS.Select(" S_FISBN='001001'")[1];
            //筛选出没标记的最后两级
            Oteher.Add("001001002", Guid.Parse("93b66eff-443e-4848-a76e-b8d4249a91d7"));
            OteherDx.Add("001001002",A2);
            foreach (DataRow item in WBS.Rows)
            {
                if (item["S_ISBN"].ToString()== "001001002002001001011004" || item["S_ISBN"].ToString()== "001001002002001001002"|| item["S_ISBN"].ToString()== "001001002002001003002001")
                {
                    string a = "";
                }
                if (item["S_ISBN"].ToString().Length<7|| item["S_ISBN"].ToString().Substring(0, 9) != "001001002"|| item["S_ISBN"].ToString()== "001001002")
                {
                    continue;
                }
                if (!Gx.ContainsKey(item["S_ISBN"].ToString())&&!Gj.ContainsKey(item["S_ISBN"].ToString())&& !Zy.ContainsKey(item["S_ISBN"].ToString()))
                {
                    DataRow []  L2 = WBS.Select(" S_FISBN='" + item["S_ISBN"] + "'");
                  
                    if (L2.Length == 0)
                    {
                        Guid guid = Guid.NewGuid();
                        Gj.Add(item["S_ISBN"].ToString(), guid);
                        GjDx.Add(item["S_ISBN"].ToString(), item);

                        //上一级为作业
                        if (!Zy.Keys.Contains(item["S_FISBN"]))
                        {
                            Guid guid1 = Guid.NewGuid();
                            DataRow[] dataRow = WBS.Select(" S_ISBN='" + item["S_FISBN"] + "'");
                            Zy.Add(dataRow[0]["S_ISBN"].ToString(), guid1);
                            ZyDx.Add(dataRow[0]["S_ISBN"].ToString(), dataRow[0]);
                        }
                    }
                    else 
                    {
                        

                            Guid guid = Guid.NewGuid();
                            if (!Oteher.Keys.Contains(item["S_ISBN"].ToString()))
                            {
                                Oteher.Add(item["S_ISBN"].ToString(), guid);
                                OteherDx.Add(item["S_ISBN"].ToString(), item);
                            }
                          
                    }
                   
                }
            }

            //构造整个关系，先找所有明确标了工序的
            //筛选出A2后的


            foreach (string item in OteherDx.Keys)
            {
                if (OteherDx[item]["S_ISBN"].ToString() == "001001002002001001011004")
                {
                    string AAA = ":";
                }
                if (dic.Keys.Contains(OteherDx[item]["S_FISBN"].ToString()))
                {
                    OteherDx[item]["S_FISBN"] = dic[OteherDx[item]["S_FISBN"].ToString()];
                }
                else
                if (!Oteher.Keys.Contains(OteherDx[item]["S_FISBN"].ToString()))
                {
                    if (Zy.Keys.Contains(OteherDx[item]["S_FISBN"].ToString()))
                    {
                        Oteher.Add(OteherDx[item]["S_FISBN"].ToString() + "-", Guid.NewGuid());
                        dic.Add(OteherDx[item]["S_FISBN"].ToString(), OteherDx[item]["S_FISBN"].ToString() + "-");
                        //捞出对象
                        DataRow dr = ZyDx[OteherDx[item]["S_FISBN"].ToString()];
                        DataRow dataRow = WBS.NewRow();
                        dataRow.ItemArray = dr.ItemArray;
                        dataRow["S_ISBN"] = OteherDx[item]["S_FISBN"].ToString() + "-";
                        ZyDx[OteherDx[item]["S_FISBN"].ToString()]["S_FISBN"] = dataRow["S_ISBN"];

                        GetWBS(dataRow, WBS);
                        OteherDx[item]["S_FISBN"] = dataRow["S_ISBN"];
                    }
                }
                GetWBS(OteherDx[item], WBS);
            }

            //处理构件,工序,zoye
            foreach (string item in ZyDx.Keys)
            {
                if (ZyDx[item]["S_FISBN"].ToString()== "001001002002001001011004")
                {
                    string aaaaa = "";
                }
                if (arrayList.Contains(item)|| !Oteher.Keys.Contains(ZyDx[item]["S_FISBN"].ToString()))
                {
                     continue;

                    //{
                    //    if (Zy.Keys.Contains(ZyDx[item]["S_FISBN"].ToString()))
                    //    {
                    //        Guid g = Guid.NewGuid();
                    //        DataRow dr = WBS.NewRow();
                    //        dr.ItemArray = ZyDx[ZyDx[item]["S_FISBN"].ToString()].ItemArray;
                    //        dr["S_ISBN"] = item + "-";
                    //        Oteher.Add(item + "-", g);
                    //        ZyDx[ZyDx[item]["S_FISBN"].ToString()]["S_FISBN"] = item + "-";
                    //        ZyDx[item]["S_FISBN"] = item + "-";
                    //        OteherDx.Add(item + "-", dr);
                    //        wbs++;
                    //        wbsid.Add(g.ToString(), wbs);

                    //        BcWBS(dr, WBS, wbs);


                    //    }
                    //    else if (Gj.Keys.Contains(ZyDx[item]["S_FISBN"].ToString()))
                    //    {
                    //        continue;
                    //    }
                        // continue;
                    }
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = Zy[item].ToString();
                lN_Task.task_id = task;
                lN_Task.wbs_id = wbsid[Oteher[ZyDx[item]["S_FISBN"].ToString()].ToString()];
                taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 5;
                lN_Task.proj_guid = "3cdf89f3-2d16-48c7-9d26-51c1a2671109";
                lN_Task.wbs_guid = Oteher[ZyDx[item]["S_FISBN"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.plan_guid = "ff794bec-1c28-4f67-9d23-ad237dd03532";
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["S_ISBN"].ToString();
                lN_Task.task_name = ZyDx[item]["S_NAME"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Tasks.Add(lN_Task);
                task++;
            }

            foreach (string item in GjDx.Keys)
            {
                if (GjDx[item]["S_FISBN"].ToString().ToString()== "001001002002001001011004004")
                {
                  continue;
                }
                PLN_taskproc lN_Taskproc = new PLN_taskproc();
                lN_Taskproc.proj_id = 5;
                lN_Taskproc.proc_id = proc;
                lN_Taskproc.proc_code = item;
                lN_Taskproc.task_id = taskid[Zy[GjDx[item]["S_FISBN"].ToString()].ToString()];
                lN_Taskproc.wbs_id = wbsid[Oteher[ZyDx[GjDx[item]["S_FISBN"].ToString()]["S_FISBN"].ToString()].ToString()];
                lN_Taskproc.task_guid = Zy[GjDx[item]["S_FISBN"].ToString()].ToString();
                lN_Taskproc.proc_name = GjDx[item]["S_NAME"].ToString();
                lN_Taskproc.proc_guid = Gj[item].ToString();
                lN_Taskproc.plan_guid = "ff794bec-1c28-4f67-9d23-ad237dd03532";
                lN_Taskproc.proj_guid = "3cdf89f3-2d16-48c7-9d26-51c1a2671109";
                lN_Taskproc.wbs_guid = Oteher[ZyDx[GjDx[item]["S_FISBN"].ToString()]["S_FISBN"].ToString()].ToString();
                lN_Taskprocs.Add(lN_Taskproc);
                proc++;
            }

            foreach (string item in GxDx.Keys)
            {
                if (!Gj.Keys.Contains(GxDx[item]["S_FISBN"].ToString()))
                {
                    //continue;
                }
                PS_PLN_TaskProc_Sub s = new PS_PLN_TaskProc_Sub();
                s.ProcSub_guid = Gx[item].ToString();
                s.ProcSub_Name = GxDx[item]["S_NAME"].ToString();
                s.ProcSub_Code =item;
                s.proj_guid = "3cdf89f3-2d16-48c7-9d26-51c1a2671109";
                s.proc_guid = Gj[GxDx[item]["S_FISBN"].ToString()].ToString();
                s.RegDate = DateTime.Now;
                s.RegHumId = Guid.Parse("AD000000-0000-0000-0000-000000000000");
                s.RegHumName = "系统管理员";
                In_TaskProc_Sub.Add(s);
            }


            ArrayList array = new ArrayList();
            //最后处理作业与WBS的联系
            foreach (PLN_task item in lN_Tasks)
            {
                if (lN_PROJWBs.Find(z=>z.wbs_short_name== item.task_code)!=null)
                {
                    item.wbs_guid = lN_PROJWBs.Find(z => z.wbs_short_name == item.task_code).wbs_guid;
                    array.Add(lN_PROJWBs.Find(z => z.wbs_short_name == item.task_code));
                }
            }
            foreach (PLN_PROJWBS item in array)
            {
                item.wbs_short_name = item.wbs_short_name + "-";
            }

            //提交数据
            DBService.Context.Insert(In_TaskProc_Sub);
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);
            DBService.Context.Insert(lN_Taskprocs);
        }


        static Dictionary<string, int> wbsid = new Dictionary<string, int>();

        static Dictionary<string, int> taskid = new Dictionary<string, int>();

        static Dictionary<string, int> procid = new Dictionary<string, int>();

        static Dictionary<string, int> procsubid = new Dictionary<string, int>();
        //递归获取下一层级
         static int wbs = 1061;
        static int task = 26463;
        static int proc = 30;
        static int procsub = 30;

        public static void GetWBS(DataRow dataRow,DataTable dt)
        {
            if (dataRow["S_NAME"].ToString()=="A2")
            {
                return;
            }

            PLN_PROJWBS WBS = new PLN_PROJWBS();
            WBS.wbs_id = wbs;
            wbsid.Add(Oteher[dataRow["S_ISBN"].ToString()].ToString(), wbs);
            WBS.wbs_guid = Oteher[dataRow["S_ISBN"].ToString()].ToString();
            WBS.proj_id = 5;
            WBS.proj_guid = "3cdf89f3-2d16-48c7-9d26-51c1a2671109";
            WBS.obs_id = 0;
            WBS.seq_num = 10;
            WBS.proj_node_flag = "0";
            WBS.sum_data_flag = "N";
            WBS.status_code = "0";
            WBS.wbs_short_name = dataRow["S_ISBN"].ToString();
            WBS.wbs_name = dataRow["S_NAME"].ToString();
            WBS.phase_id = 0;

                WBS.parent_wbs_guid = Oteher[dataRow["S_FISBN"].ToString()].ToString();
            WBS.guid = Oteher[dataRow["S_ISBN"].ToString()].ToString();
            WBS.update_date = DateTime.Now;
            WBS.plan_guid = "ff794bec-1c28-4f67-9d23-ad237dd03532";
            lN_PROJWBs.Add(WBS);
            wbs++;
        }



        public static void BcWBS(DataRow dataRow, DataTable dt,int wbsidc)
        {
            if (dataRow["S_NAME"].ToString() == "A2")
            {
                return;
            }
            PLN_PROJWBS WBS = new PLN_PROJWBS();
            WBS.wbs_id = wbsidc;
            WBS.wbs_guid = Oteher[dataRow["S_ISBN"].ToString()].ToString();
            WBS.proj_id = 5;
            WBS.proj_guid = "3cdf89f3-2d16-48c7-9d26-51c1a2671109";
            WBS.obs_id = 0;
            WBS.seq_num = 10;
            WBS.proj_node_flag = "0";
            WBS.sum_data_flag = "N";
            WBS.status_code = "0";
            WBS.wbs_short_name = dataRow["S_ISBN"].ToString();
            WBS.wbs_name = dataRow["S_NAME"].ToString();
            WBS.phase_id = 0;

            WBS.parent_wbs_guid = Oteher[dataRow["S_FISBN"].ToString()].ToString();
            WBS.guid = Oteher[dataRow["S_ISBN"].ToString()].ToString();
            WBS.update_date = DateTime.Now;
            WBS.plan_guid = "ff794bec-1c28-4f67-9d23-ad237dd03532";
            lN_PROJWBs.Add(WBS);
            wbs++;
        }


        public  static void ChangeZy()
        {
             List<PLN_PROJWBS> lN_PROJWBs = DBService.Context.FromSql("SELECT  *FROM   PLN_PROJWBS WHERE wbs_short_name like '%-'").ToList<PLN_PROJWBS>();
             List<PLN_task> lN_Tasks = DBService.Context.FromSql(" select * from PLN_task").ToList<PLN_task>();
            List<PLN_taskproc> lN_Taskprocs = DBService.Context.FromSql(" select * from PLN_taskproc").ToList<PLN_taskproc>();
            List<PLN_task> ins = new List<PLN_task>();
            List<PLN_task> Des = new List<PLN_task>();

            DataTable dt = DBService.Context.FromSql(" select MAX(task_id) as id from PLN_task").ToDataTable();
            int MAXID = Convert.ToInt32(dt.Rows[0]["id"].ToString())+1;
            //重新梳理Code
            foreach (PLN_PROJWBS item in lN_PROJWBs)
            {
                item.wbs_short_name = item.wbs_short_name.Split('-')[0];
                //找出后续的
                PLN_task task = lN_Tasks.Find(z=>z.wbs_guid==item.wbs_guid);

                Des.Add(task);
                //找出所有构件，提高一层级
                List<PLN_taskproc> taskproc = lN_Taskprocs.FindAll(z=>z.task_guid==task.task_guid);

                foreach (PLN_taskproc child in taskproc)
                {
                    string ID= Guid.NewGuid().ToString(); 
                    PLN_task instask = new PLN_task();
                    instask.task_guid = ID;
                    instask.task_id = MAXID;
                    instask.wbs_id = child.wbs_id;
                    instask.proj_id = 5;
                    instask.proj_guid = "3cdf89f3-2d16-48c7-9d26-51c1a2671109";
                    instask.wbs_guid =child.wbs_guid;
                    instask.clndr_id = 0;
                    instask.plan_guid = "ff794bec-1c28-4f67-9d23-ad237dd03532";
                    instask.parent_guid = "00000000-0000-0000-0000-000000000000";
                    instask.rev_fdbk_flag = "N";
                    instask.lock_plan_flag = "N";
                    instask.complete_pct_type = "CP_Drtn";
                    instask.task_type = "TT_Task";
                    instask.status_code = "TK_NotStart";
                    instask.task_code = child.proc_code+"-";
                    instask.task_name =child.proc_name;
                    instask.guid = ID;
                    ins.Add(instask);

                    child.task_id = MAXID;
                    child.task_guid = ID;
                    MAXID++;
                  
                }
            }


            DBService.Context.Update<PLN_PROJWBS>(lN_PROJWBs);
            DBService.Context.Insert<PLN_task>(ins);
            DBService.Context.Delete<PLN_task>(Des);
            DBService.Context.Update<PLN_taskproc>(lN_Taskprocs);
        }

        public static void UpWBSTargetDate()
        {
        }

        public static void GetA1Rq()//更新作业的时间和原定工期
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from  PLN_task where plan_guid='"+ A1NowPlan_guid + "'").ToList<PLN_task>();//现在的
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001001%' and plan_guid= '"+ A1BeforePlan_guid + "'  ").ToList<PLN_task>();//原来的
            List<PLN_task> newtask = new List<PLN_task>();
            foreach (PLN_task item in task)
            {
                PLN_task pLN_Task = OldTask.Find(z => z.task_code == item.task_code);
                if (pLN_Task != null)
                {
                    item.target_drtn_hr_cnt = pLN_Task.target_drtn_hr_cnt;
                    item.target_start_date = pLN_Task.target_start_date;
                    item.target_end_date = pLN_Task.target_end_date;
                    newtask.Add(item);
                }
            }
            DBService.Context.Update(newtask);

        }

        public static void GetA2Rq()//更新作业的时间和原定工期
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from  PLN_task where plan_guid='"+ A2NowPlan_guid + "'").ToList<PLN_task>();//现在的
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001002%' and plan_guid= '"+ A2BeforePlan_guid + "'  ").ToList<PLN_task>();//原来的
            List<PLN_task> newtask = new List<PLN_task>();
            foreach (PLN_task item in task)
            {
                PLN_task pLN_Task = OldTask.Find(z => z.task_code == item.task_code);
                if (pLN_Task != null)
                {
                    item.target_drtn_hr_cnt = pLN_Task.target_drtn_hr_cnt;
                    item.target_start_date = pLN_Task.target_start_date;
                    item.target_end_date = pLN_Task.target_end_date;
                    newtask.Add(item);
                }
            }
            DBService.Context.Update(newtask);

        }


        public static void GetA1Lj()//刷新逻辑关系
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from  PLN_task where plan_guid='"+A1NowPlan_guid+"'").ToList<PLN_task>();
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001001%' and  plan_guid= '"+A1BeforePlan_guid+"' ").ToList<PLN_task>();
            List<PLN_TASKPRED> newpred = new List<PLN_TASKPRED>();

            List<PLN_TASKPRED> Old = DBService.Context.FromSql("select * from pln_taskpred where  plan_guid='"+ A1BeforePlan_guid + "'").ToList<PLN_TASKPRED>();

            int num = 200;
            foreach (PLN_TASKPRED item in Old)
            {
                num++;
                PLN_TASKPRED a = new PLN_TASKPRED();

                PLN_task oldItem = OldTask.Find(z => z.task_guid == item.task_guid);
                if (oldItem != null)
                {
                    PLN_task pLN_Task = task.Find(z => z.task_code == oldItem.task_code);
                    if (pLN_Task != null)
                    {
                        a.task_pred_id = num;
                        a.task_id = pLN_Task.task_id;
                        a.wbs_id = pLN_Task.wbs_id;

                        PLN_task jq = GetJq(task, OldTask, item);
                        if (jq != null)
                        {
                            a.pred_task_id = jq.task_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.proj_id = pLN_Task.proj_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.pred_proj_id = jq.proj_id;
                            a.pred_type = item.pred_type;
                            a.lag_hr_cnt = item.lag_hr_cnt;
                            a.update_date = item.update_date;
                            a.update_user = item.update_user;
                            a.create_date = item.create_date;
                            a.create_user = item.create_user;
                            a.delete_session_id = item.delete_session_id;
                            a.delete_date = item.delete_date;
                            a.temp_id = a.temp_id;
                            a.p3ec_flag = item.p3ec_flag;
                            a.p3ec_pred_id = item.p3ec_pred_id;
                            a.task_guid = pLN_Task.task_guid;
                            a.pred_guid = jq.task_guid;
                            a.control_path_flag = item.control_path_flag;
                            a.plan_id = pLN_Task.plan_id;
                            a.pred_plan_id = jq.plan_id;
                            a.task_pred_guid = Guid.NewGuid().ToString();
                            a.wbs_guid = pLN_Task.wbs_guid;
                            a.pred_wbs_guid = jq.wbs_guid;
                            a.proj_guid = pLN_Task.proj_guid;
                            a.plan_guid = pLN_Task.plan_guid;
                            a.pred_proj_guid = jq.proj_guid;
                            a.pred_plan_guid = jq.plan_guid;
                            a.temp_guid = item.temp_guid;
                            newpred.Add(a);
                        }
                    }
                }
            }


            DBService.Context.Insert(newpred);

        }

       


        public static void GetA2Lj()//刷新逻辑关系
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from  PLN_task where plan_guid='"+A2NowPlan_guid+"'").ToList<PLN_task>();
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001002%' and  plan_guid= '"+A2BeforePlan_guid+"' ").ToList<PLN_task>();
            List<PLN_TASKPRED> newpred = new List<PLN_TASKPRED>();

            List<PLN_TASKPRED> Old = DBService.Context.FromSql("select * from pln_taskpred where  plan_guid='"+ A2BeforePlan_guid + "'").ToList<PLN_TASKPRED>();

            int num = 200;
            foreach (PLN_TASKPRED item in Old)
            {
                num++;
                PLN_TASKPRED a = new PLN_TASKPRED();

                PLN_task oldItem = OldTask.Find(z => z.task_guid == item.task_guid);
                if (oldItem != null)
                {
                    PLN_task pLN_Task = task.Find(z => z.task_code == oldItem.task_code);
                    if (pLN_Task != null)
                    {
                        a.task_pred_id = num;
                        a.task_id = pLN_Task.task_id;
                        a.wbs_id = pLN_Task.wbs_id;

                        PLN_task jq = GetJq(task, OldTask, item);
                        if (jq != null)
                        {
                            a.pred_task_id = jq.task_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.proj_id = pLN_Task.proj_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.pred_proj_id = jq.proj_id;
                            a.pred_type = item.pred_type;
                            a.lag_hr_cnt = item.lag_hr_cnt;
                            a.update_date = item.update_date;
                            a.update_user = item.update_user;
                            a.create_date = item.create_date;
                            a.create_user = item.create_user;
                            a.delete_session_id = item.delete_session_id;
                            a.delete_date = item.delete_date;
                            a.temp_id = a.temp_id;
                            a.p3ec_flag = item.p3ec_flag;
                            a.p3ec_pred_id = item.p3ec_pred_id;
                            a.task_guid = pLN_Task.task_guid;
                            a.pred_guid = jq.task_guid;
                            a.control_path_flag = item.control_path_flag;
                            a.plan_id = pLN_Task.plan_id;
                            a.pred_plan_id = jq.plan_id;
                            a.task_pred_guid = Guid.NewGuid().ToString();
                            a.wbs_guid = pLN_Task.wbs_guid;
                            a.pred_wbs_guid = jq.wbs_guid;
                            a.proj_guid = pLN_Task.proj_guid;
                            a.plan_guid = pLN_Task.plan_guid;
                            a.pred_proj_guid = jq.proj_guid;
                            a.pred_plan_guid = jq.plan_guid;
                            a.temp_guid = item.temp_guid;
                            newpred.Add(a);
                        }
                    }
                }
            }


            DBService.Context.Insert(newpred);

        }


        public static PLN_task GetJq(List<PLN_task> task, List<PLN_task> OldTask, PLN_TASKPRED _TASKPRED)
        {
            PLN_task oldItem = OldTask.Find(z => z.task_guid == _TASKPRED.pred_guid);
            if (oldItem != null)
            {
                PLN_task pLN_Task = task.Find(z => z.task_code == oldItem.task_code);
                if (pLN_Task != null)
                {
                    return pLN_Task;

                }
            }
            return null;
        }


        public static PLN_Tepmtask GetJqTemp(List<PLN_Tepmtask> task, List<PLN_task> OldTask, PLN_TASKPRED _TASKPRED)
        {
            PLN_task oldItem = OldTask.Find(z => z.task_guid == _TASKPRED.pred_guid);
            if (oldItem != null)
            {
                PLN_Tepmtask pLN_Task = task.Find(z => z.task_code == oldItem.task_code);
                if (pLN_Task != null)
                {
                    return pLN_Task;

                }
            }
            return null;
        }


        //public static void updateWBSHs()
        //{
        //    DataTable dataTable = DBService.Context.FromSql("select * from PLN_PROJWBS ").ToDataTable();//现有的wbs层级

        //    DataTable dtZy = DBService.My.FromSql(" select id,wbs_id,wbs_name,parent_wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and wbs_id like '001001001%' and wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id) ").ToDataTable();

        //    foreach (DataRow item in dataTable.Rows)
        //    {
        //        Guid guid = Guid.NewGuid();
        //        Oteher.Add(item["wbs_id"].ToString(), guid);
        //        OteherDx.Add(item["wbs_id"].ToString(), item);
        //    }

        //    foreach (DataTable item in dtZy.Rows)
        //    {
        //        Guid guid = Guid.NewGuid();
        //        PLN_task lN_Task = new PLN_task();
        //        lN_Task.task_guid = guid.ToString();
        //        lN_Task.task_id = task;
        //        lN_Task.wbs_id = OteherDx[item["wbs_id"].ToString()];
        //        taskid.Add(Zy[item].ToString(), task);
        //        lN_Task.proj_id = 5;
        //        lN_Task.plan_id = 1018;
        //        lN_Task.proj_guid = "1cc767c9-ec0f-403c-ad37-616487cc5139";//需要获取对应标段
        //        lN_Task.plan_guid = "7c099591-6068-371f-218f-5ac51a3aa531";//需要获取对应标段
        //        lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
        //        lN_Task.clndr_id = 0;
        //        lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
        //        lN_Task.rev_fdbk_flag = "N";
        //        lN_Task.lock_plan_flag = "N";
        //        lN_Task.complete_pct_type = "CP_Drtn";
        //        lN_Task.task_type = "TT_Task";
        //        lN_Task.status_code = "TK_NotStart";
        //        lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
        //        lN_Task.task_name = ZyDx[item]["wbs_name"].ToString();
        //        lN_Task.guid = Zy[item].ToString();
        //        lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
        //        lN_Tasks.Add(lN_Task);
        //        task++;
        //    }
        //}

        public static void SelWBS()//查询遗漏的wbs和作业
        {
            //查询出华设视图中所有的wbs+作业
            DataTable dtZy = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs ) " +
                " and wbs_id like '001001001%' and wbs_id<> '001001001'" +
                " union all " +
                " select * from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and " +
                " wbs_id like '001001001%' and wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id)  ").ToDataTable();


            DataTable dataTable = DBService.Context.FromSql("select * from pln_task where plan_guid='72d65f18-6bb8-cf61-997e-3c0c1ab2edb4' ").ToDataTable();//查询出阿里云数据库中的所有的作业

            for (int i = 0; i < dtZy.Rows.Count; i++)
            {
                DataRow[] dr=dataTable.Select(" task_code='" + dtZy.Rows[i]["wbs_id"].ToString().Trim() + "'");
                if (dr.Length==0)
                {
                    string s = dtZy.Rows[i]["wbs_id"].ToString().Trim();
                }
            }

        }
        public static void GetA1WBSHsTemp()//通过临时表的方式将华设更新的数据读取
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  " + //wbs层级
            "                                       and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1 ").ToDataTable();//A1标段是001001001，A2标段是001001002

            //此方法处理将ebs复制到wbs中的方法
            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 ) 
                 and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1
                 union all 
				 select 2 as XH,A.wbs_id,A.wbs_name,A.parent_wbs_id from view_bim_wbs A where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  and 
                A.wbs_id like '001001001%' and A.wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id and is_deleted<>1))   ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            List<PLN_task> NowTasks = DBService.Context.FromSql("select * from  PLN_task where plan_guid='"+A1NowPlan_guid+"'").ToList<PLN_task>();//查询临时表现有的作业

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (Zy.ContainsKey(item["wbs_id"].ToString()))
                {
                    continue;
                }
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_TempPROJWBS WBS = new PLN_TempPROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = Proj_id;
                WBS.plan_id = A1Plan_id;
                WBS.proj_guid = NowProj_guid;//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = A1parent_wbs_guid;//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = A1NowPlan_guid;//需要获取对应标段
                lN_TempPROJWBs.Add(WBS);
                wbs++;
            }

            foreach (DataRow data in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                string item = data["wbs_id"].ToString();
                PLN_Tepmtask lN_Task = new PLN_Tepmtask();
                PLN_task TempNowTasks = NowTasks.Find(z => z.task_code == ZyDx[item]["wbs_id"].ToString());
                if(TempNowTasks!=null)
                    lN_Task.task_guid = TempNowTasks.task_guid;
                else
                  lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !data["XH"].ToString().Equals("2"))
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                    lN_Task.task_name = data["wbs_name"].ToString();
                }
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.task_name = "基础作业";
                    //lN_Task.TempTask_code = data["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = Proj_id;
                lN_Task.plan_id = A1Plan_id;
                lN_Task.proj_guid = NowProj_guid;//需要获取对应标段
                lN_Task.plan_guid = A1NowPlan_guid;//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                //lN_Task.task_name = data["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_TempTasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_TempPROJWBs);
            DBService.Context.Insert(lN_TempTasks);

            sSQL.Clear();
            sSQL.AppendLine("update PLN_TempPROJWBS set parent_wbs_id =(select wbs_id from PLN_TempPROJWBS B where PLN_TempPROJWBS.parent_wbs_guid=B.wbs_guid) where plan_guid='" + A1NowPlan_guid + "' ");
            dbSQL.doSQL(sSQL.ToString());


            #region 将原定工期、计划开始、结束时间刷到临时表中
            A1UpdateYDGQToTemp();
            #endregion

            #region 将逻辑关系刷新到临时表中
            //A1UpLJGXToTemp();
            #endregion

            #region 删除现有的作业表、wbs表,再将临时表中的数据插入
            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_task where plan_guid='"+ A1NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_PROJWBS where plan_guid='"+ A1NowPlan_guid + "' and (wbs_short_name<>'A1' and wbs_short_name<>'JJSD' )");
            dbSQL.doSQL(sSQL.ToString());

            //插入wbs表
            sSQL.Clear();
            sSQL.AppendLine(InsertNowProjWbs);
            dbSQL.doSQL(sSQL.ToString());

            //插入作业表
            sSQL.Clear();
            sSQL.AppendLine(InsertNowTask);
            dbSQL.doSQL(sSQL.ToString());
            #endregion

            #region 删除临时表
            sSQL.Clear();
            sSQL.AppendLine("delete from pln_tempprojwbs where plan_guid='" + A1NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_tepmtask where plan_guid='" + A1NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());
            #endregion
        }

        public static void GetA2WBSHsTemp()//通过临时表的方式将华设更新的数据读取
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  " + //wbs层级
            "                                       and wbs_id like '001001002%' and wbs_id<> '001001002' and is_deleted<>1 ").ToDataTable();//A1标段是001001001，A2标段是001001002

            //此方法处理将ebs复制到wbs中的方法
            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 ) 
                 and wbs_id like '001001002%' and wbs_id<> '001001002' and is_deleted<>1
                 union all 
				 select 2 as XH,A.wbs_id,A.wbs_name,A.parent_wbs_id from view_bim_wbs A where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  and 
                A.wbs_id like '001001002%' and A.wbs_id<> '001001002' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id and is_deleted<>1))   ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            List<PLN_task> NowTasks = DBService.Context.FromSql("select * from  PLN_task where plan_guid='" + A2NowPlan_guid + "'").ToList<PLN_task>();//查询临时表现有的作业

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (Zy.ContainsKey(item["wbs_id"].ToString()))
                {
                    continue;
                }
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_TempPROJWBS WBS = new PLN_TempPROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = Proj_id;
                WBS.plan_id = A2Plan_id;
                WBS.proj_guid = NowProj_guid;//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = A2parent_wbs_guid;//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = A2NowPlan_guid;//需要获取对应标段
                lN_TempPROJWBs.Add(WBS);
                wbs++;
            }

            foreach (DataRow data in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                string item = data["wbs_id"].ToString();
                PLN_Tepmtask lN_Task = new PLN_Tepmtask();
                PLN_task TempNowTasks = NowTasks.Find(z => z.task_code == ZyDx[item]["wbs_id"].ToString());
                if (TempNowTasks != null)
                    lN_Task.task_guid = TempNowTasks.task_guid;
                else
                    lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !data["XH"].ToString().Equals("2"))
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                    lN_Task.task_name = data["wbs_name"].ToString();
                }
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.task_name = "基础作业";
                    //lN_Task.TempTask_code = data["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = Proj_id;
                lN_Task.plan_id = A2Plan_id;
                lN_Task.proj_guid = NowProj_guid;//需要获取对应标段
                lN_Task.plan_guid = A2NowPlan_guid;//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                //lN_Task.task_name = data["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_TempTasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_TempPROJWBs);
            DBService.Context.Insert(lN_TempTasks);

            sSQL.Clear();
            sSQL.AppendLine("update PLN_TempPROJWBS set parent_wbs_id =(select wbs_id from PLN_TempPROJWBS B where PLN_TempPROJWBS.parent_wbs_guid=B.wbs_guid) where plan_guid='" + A2NowPlan_guid + "' ");
            dbSQL.doSQL(sSQL.ToString());


            #region 将原定工期、计划开始、结束时间刷到临时表中
            A2UpdateYDGQToTemp();
            #endregion

            #region 将逻辑关系刷新到临时表中
            A2UpLJGXToTemp();
            #endregion

            #region 删除现有的作业表、wbs表,再将临时表中的数据插入
            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_task where plan_guid='" + A2NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_PROJWBS where plan_guid='" + A2NowPlan_guid + "' and (wbs_short_name<>'A2' and wbs_short_name<>'JJSD' )");
            dbSQL.doSQL(sSQL.ToString());

            string InsertNowProjWbs = @"insert into pln_projwbs (wbs_id,wbs_guid,proj_id,proj_guid,obs_id,obs_guid,seq_num,est_wt,complete_pct,proj_node_flag,sum_data_flag,status_code,wbs_short_name,wbs_name,phase_id,parent_wbs_id,parent_wbs_guid,ev_user_pct,ev_etc_user_value,orig_cost,user_cost1,indep_remain_total_cost,
                                            ann_dscnt_rate_pct,dscnt_period_type,indep_remain_work_qty,anticip_start_date,anticip_end_date,ev_compute_type,ev_etc_compute_type,guid,tmpl_guid,p3ec_wbs_id,p3ec_parentwbs_id,p3ec_flag,treelevel,haschild,discolor,target_start_date,target_end_date,expect_start_date,expect_end_date,
                                            act_start_date,act_end_date,SysOrNot,allowModifyTaskOrNot,complete_pct_type,target_drtn_hr_cnt,remain_drtn_hr_cnt,update_date,start_date,end_date,LongCode,plan_id,plan_guid,est_wt_pct,require_start_date,require_end_date,main_wbs_guid,bcws_cost,bcwp_cost,
                                            acwp_cost,budget_cost,reghumanid,reghumanname,rsrc_guid,rsrc_name,responsibleid,responsiblename,update_user,create_date,create_user,delete_session_id,delete_date,target_complete_pct,period_target_complete_pct,
                                            restart_date,reend_date,plan_pct,wbs_guid_before,dept_name,dept_guid,remark,plan_hour,act_hour)
                                            select wbs_id,wbs_guid,proj_id,proj_guid,obs_id,obs_guid,seq_num,est_wt,complete_pct,proj_node_flag,sum_data_flag,status_code,wbs_short_name,wbs_name,phase_id,parent_wbs_id,parent_wbs_guid,ev_user_pct,ev_etc_user_value,orig_cost,user_cost1,indep_remain_total_cost,
                                            ann_dscnt_rate_pct,dscnt_period_type,indep_remain_work_qty,anticip_start_date,anticip_end_date,ev_compute_type,ev_etc_compute_type,guid,tmpl_guid,p3ec_wbs_id,p3ec_parentwbs_id,p3ec_flag,treelevel,haschild,discolor,target_start_date,target_end_date,expect_start_date,expect_end_date,
                                            act_start_date,act_end_date,SysOrNot,allowModifyTaskOrNot,complete_pct_type,target_drtn_hr_cnt,remain_drtn_hr_cnt,update_date,start_date,end_date,LongCode,plan_id,plan_guid,est_wt_pct,require_start_date,require_end_date,main_wbs_guid,bcws_cost,bcwp_cost,
                                            acwp_cost,budget_cost,reghumanid,reghumanname,rsrc_guid,rsrc_name,responsibleid,responsiblename,update_user,create_date,create_user,delete_session_id,delete_date,target_complete_pct,period_target_complete_pct,
                                            restart_date,reend_date,plan_pct,wbs_guid_before,dept_name,dept_guid,remark,plan_hour,act_hour from pln_tempprojwbs where plan_guid='" + A2NowPlan_guid + "'";

            string InsertNowTask = @"insert into PLN_task(task_guid,proj_id,proj_guid,wbs_id,wbs_guid,clndr_id,clndr_guid,plan_id,plan_guid,
                                        seq_num,parent_task_id,parent_guid,est_wt,complete_pct,rev_fdbk_flag,lock_plan_flag,auto_compute_act_flag,complete_pct_type,
                                        task_type,duration_type,review_type,status_code,task_code,task_name,rsrc_name,rsrc_id,rsrc_guid,total_float_hr_cnt,free_float_hr_cnt,
                                        target_drtn_hr_cnt,act_drtn_hr_cnt,remain_drtn_hr_cnt,act_work_qty,remain_work_qty,target_work_qty,target_equip_qty,act_equip_qty,remain_equip_qty,
                                        cstr_type,cstr_date,late_start_date,late_end_date,early_start_date,early_end_date,restart_date,reend_date,review_end_date,rem_late_start_date,rem_late_end_date,
                                        priority_type,guid,tmpl_guid,cstr_date2,cstr_type2,act_this_per_work_qty,act_this_per_equip_qty,driving_path_flag,float_path,float_path_order,suspend_date,resume_date,
                                        external_early_start_date,external_late_end_date,update_date,update_user,create_date,create_user_sid,create_user,delete_session_id,delete_date,act_start_date,act_end_date,
                                        expect_start_date,expect_end_date,target_start_date,target_end_date,SysOrNot,rec_type,temp_id,p3ec_flag,p3ec_task_id,data_date,start_date,end_date,plan_task_id_befor,plan_task_guid_before,
                                        memo,module_type,plan_actvcode_guid,plan_actvcode_id,parent_task_plan_guid,est_wt_pct,curve_guid,feedback_pct_type,baseline_start_date,baseline_end_date,baseline2_start_date,baseline2_end_date,
                                        baseline3_start_date,baseline3_end_date,progress_rsrc_unit_name,progress_rsrc_unit_price,target_progress_rsrc_cost,act_progress_rsrc_cost,remain_progress_rsrc_cost,target_progress_rsrc_curv,
                                        act_progress_rsrc_curv,remain_progress_rsrc_curv,target_progress_rsrc_guid,target_progress_rsrc_code,target_progress_rsrc_qty,act_progress_rsrc_qty,remain_progress_rsrc_qty,videoUrl,bcws_cost,bcwp_cost,acwp_cost,
                                        istopbreakdown,budget_cost,attach_flag,UPDATE_SESSION_ID,target_complete_pct,period_target_complete_pct,important_node_flag,plan_pct,feedback_data_date,dept_name,dept_guid,remark,plan_hour,act_hour,TempTask_code)
                                        select task_guid,proj_id,proj_guid,wbs_id,wbs_guid,clndr_id,clndr_guid,plan_id,plan_guid,
                                        seq_num,parent_task_id,parent_guid,est_wt,complete_pct,rev_fdbk_flag,lock_plan_flag,auto_compute_act_flag,complete_pct_type,
                                        task_type,duration_type,review_type,status_code,task_code,task_name,rsrc_name,rsrc_id,rsrc_guid,total_float_hr_cnt,free_float_hr_cnt,
                                        target_drtn_hr_cnt,act_drtn_hr_cnt,remain_drtn_hr_cnt,act_work_qty,remain_work_qty,target_work_qty,target_equip_qty,act_equip_qty,remain_equip_qty,
                                        cstr_type,cstr_date,late_start_date,late_end_date,early_start_date,early_end_date,restart_date,reend_date,review_end_date,rem_late_start_date,rem_late_end_date,
                                        priority_type,guid,tmpl_guid,cstr_date2,cstr_type2,act_this_per_work_qty,act_this_per_equip_qty,driving_path_flag,float_path,float_path_order,suspend_date,resume_date,
                                        external_early_start_date,external_late_end_date,update_date,update_user,create_date,create_user_sid,create_user,delete_session_id,delete_date,act_start_date,act_end_date,
                                        expect_start_date,expect_end_date,target_start_date,target_end_date,SysOrNot,rec_type,temp_id,p3ec_flag,p3ec_task_id,data_date,start_date,end_date,plan_task_id_befor,plan_task_guid_before,
                                        memo,module_type,plan_actvcode_guid,plan_actvcode_id,parent_task_plan_guid,est_wt_pct,curve_guid,feedback_pct_type,baseline_start_date,baseline_end_date,baseline2_start_date,baseline2_end_date,
                                        baseline3_start_date,baseline3_end_date,progress_rsrc_unit_name,progress_rsrc_unit_price,target_progress_rsrc_cost,act_progress_rsrc_cost,remain_progress_rsrc_cost,target_progress_rsrc_curv,
                                        act_progress_rsrc_curv,remain_progress_rsrc_curv,target_progress_rsrc_guid,target_progress_rsrc_code,target_progress_rsrc_qty,act_progress_rsrc_qty,remain_progress_rsrc_qty,videoUrl,bcws_cost,bcwp_cost,acwp_cost,
                                        istopbreakdown,budget_cost,attach_flag,UPDATE_SESSION_ID,target_complete_pct,period_target_complete_pct,important_node_flag,plan_pct,feedback_data_date,dept_name,dept_guid,remark,plan_hour,act_hour,TempTask_code from PLN_tepmtask
                                        where plan_guid='" + A2NowPlan_guid + "'";

            //插入wbs表
            sSQL.Clear();
            sSQL.AppendLine(InsertNowProjWbs);
            dbSQL.doSQL(sSQL.ToString());

            //插入作业表
            sSQL.Clear();
            sSQL.AppendLine(InsertNowTask);
            dbSQL.doSQL(sSQL.ToString());
            #endregion

            #region 删除临时表
            sSQL.Clear();
            sSQL.AppendLine("delete from pln_tempprojwbs where plan_guid='" + A2NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_tepmtask where plan_guid='" + A2NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());
            #endregion
        }
        public static void A1UpdateYDGQToTemp()
        {
            List<PLN_Tepmtask> Tepmtask = DBService.Context.FromSql("select * from  PLN_Tepmtask where plan_guid='"+ A1NowPlan_guid + "'").ToList<PLN_Tepmtask>();//查询临时表现有的作业
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001001%' and plan_guid= '" + A1NowPlan_guid + "'  ").ToList<PLN_task>();//查询原来的作业表中的数据
            List<PLN_Tepmtask> newtask = new List<PLN_Tepmtask>();
            foreach (PLN_Tepmtask item in Tepmtask)
            {
                PLN_task pLN_Task = OldTask.Find(z => z.task_code == item.task_code);
                if (pLN_Task != null)
                {
                    item.target_drtn_hr_cnt = pLN_Task.target_drtn_hr_cnt;
                    item.target_start_date = pLN_Task.target_start_date;
                    item.target_end_date = pLN_Task.target_end_date;
                    newtask.Add(item);
                }
            }
            DBService.Context.Update(newtask);//将原定工期、计划开始、结束时间刷到临时表中
        }
        public static void A2UpdateYDGQToTemp()
        {
            List<PLN_Tepmtask> Tepmtask = DBService.Context.FromSql("select * from  PLN_Tepmtask where plan_guid='" + A2NowPlan_guid + "'").ToList<PLN_Tepmtask>();//查询临时表现有的作业
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001002%' and plan_guid= '" + A2NowPlan_guid + "'  ").ToList<PLN_task>();//查询原来的作业表中的数据
            List<PLN_Tepmtask> newtask = new List<PLN_Tepmtask>();
            foreach (PLN_Tepmtask item in Tepmtask)
            {
                PLN_task pLN_Task = OldTask.Find(z => z.task_code == item.task_code);
                if (pLN_Task != null)
                {
                    item.target_drtn_hr_cnt = pLN_Task.target_drtn_hr_cnt;
                    item.target_start_date = pLN_Task.target_start_date;
                    item.target_end_date = pLN_Task.target_end_date;
                    newtask.Add(item);
                }
            }
            DBService.Context.Update(newtask);//将原定工期、计划开始、结束时间刷到临时表中
        }

        public static void A1UpLJGXToTemp()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            List<PLN_Tepmtask> task = DBService.Context.FromSql("select * from  PLN_Tepmtask where plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87'").ToList<PLN_Tepmtask>();
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001001%' and  plan_guid= 'e223174f-c611-d125-a275-b3ca7d70eb87' ").ToList<PLN_task>();
            List<PLN_TASKPRED> newpred = new List<PLN_TASKPRED>();

            List<PLN_TASKPRED> Old = DBService.Context.FromSql("select * from pln_taskpred where  plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87'").ToList<PLN_TASKPRED>();

            int num = 200;
            foreach (PLN_TASKPRED item in Old)
            {
                num++;
                PLN_TASKPRED a = new PLN_TASKPRED();

                PLN_task oldItem = OldTask.Find(z => z.task_guid == item.task_guid);
                if (oldItem != null)
                {
                    PLN_Tepmtask pLN_Task = task.Find(z => z.task_code == oldItem.task_code);
                    if (pLN_Task != null)
                    {
                        a.task_pred_id = num;
                        a.task_id = pLN_Task.task_id;
                        a.wbs_id = pLN_Task.wbs_id;

                        PLN_Tepmtask jq = GetJqTemp(task, OldTask, item);
                        if (jq != null)
                        {
                            a.pred_task_id = jq.task_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.proj_id = pLN_Task.proj_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.pred_proj_id = jq.proj_id;
                            a.pred_type = item.pred_type;
                            a.lag_hr_cnt = item.lag_hr_cnt;
                            a.update_date = item.update_date;
                            a.update_user = item.update_user;
                            a.create_date = item.create_date;
                            a.create_user = item.create_user;
                            a.delete_session_id = item.delete_session_id;
                            a.delete_date = item.delete_date;
                            a.temp_id = a.temp_id;
                            a.p3ec_flag = item.p3ec_flag;
                            a.p3ec_pred_id = item.p3ec_pred_id;
                            a.task_guid = pLN_Task.task_guid;
                            a.pred_guid = jq.task_guid;
                            a.control_path_flag = item.control_path_flag;
                            a.plan_id = pLN_Task.plan_id;
                            a.pred_plan_id = jq.plan_id;
                            a.task_pred_guid = Guid.NewGuid().ToString();
                            a.wbs_guid = pLN_Task.wbs_guid;
                            a.pred_wbs_guid = jq.wbs_guid;
                            a.proj_guid = pLN_Task.proj_guid;
                            a.plan_guid = pLN_Task.plan_guid;
                            a.pred_proj_guid = jq.proj_guid;
                            a.pred_plan_guid = jq.plan_guid;
                            a.temp_guid = item.temp_guid;
                            newpred.Add(a);
                        }
                    }
                }
            }

            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_TASKPRED where plan_guid='" + A1NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            DBService.Context.Insert(newpred);
        }

        public static void A2UpLJGXToTemp()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            List<PLN_Tepmtask> task = DBService.Context.FromSql("select * from  PLN_Tepmtask where plan_guid='" + A2NowPlan_guid + "'").ToList<PLN_Tepmtask>();
            List<PLN_task> OldTask = DBService.Context.FromSql("select * from  PLN_task where task_code like '001001002%' and  plan_guid= '" + A2NowPlan_guid + "' ").ToList<PLN_task>();
            List<PLN_TASKPRED> newpred = new List<PLN_TASKPRED>();

            List<PLN_TASKPRED> Old = DBService.Context.FromSql("select * from pln_taskpred where  plan_guid='" + A2NowPlan_guid + "'").ToList<PLN_TASKPRED>();
            int num = 200;
            foreach (PLN_TASKPRED item in Old)
            {
                num++;
                PLN_TASKPRED a = new PLN_TASKPRED();

                PLN_task oldItem = OldTask.Find(z => z.task_guid == item.task_guid);
                if (oldItem != null)
                {
                    PLN_Tepmtask pLN_Task = task.Find(z => z.task_code == oldItem.task_code);
                    if (pLN_Task != null)
                    {
                        a.task_pred_id = num;
                        a.task_id = pLN_Task.task_id;
                        a.wbs_id = pLN_Task.wbs_id;

                        PLN_Tepmtask jq = GetJqTemp(task, OldTask, item);
                        if (jq != null)
                        {
                            a.pred_task_id = jq.task_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.proj_id = pLN_Task.proj_id;
                            a.pred_wbs_id = jq.wbs_id;
                            a.pred_proj_id = jq.proj_id;
                            a.pred_type = item.pred_type;
                            a.lag_hr_cnt = item.lag_hr_cnt;
                            a.update_date = item.update_date;
                            a.update_user = item.update_user;
                            a.create_date = item.create_date;
                            a.create_user = item.create_user;
                            a.delete_session_id = item.delete_session_id;
                            a.delete_date = item.delete_date;
                            a.temp_id = a.temp_id;
                            a.p3ec_flag = item.p3ec_flag;
                            a.p3ec_pred_id = item.p3ec_pred_id;
                            a.task_guid = pLN_Task.task_guid;
                            a.pred_guid = jq.task_guid;
                            a.control_path_flag = item.control_path_flag;
                            a.plan_id = pLN_Task.plan_id;
                            a.pred_plan_id = jq.plan_id;
                            a.task_pred_guid = Guid.NewGuid().ToString();
                            a.wbs_guid = pLN_Task.wbs_guid;
                            a.pred_wbs_guid = jq.wbs_guid;
                            a.proj_guid = pLN_Task.proj_guid;
                            a.plan_guid = pLN_Task.plan_guid;
                            a.pred_proj_guid = jq.proj_guid;
                            a.pred_plan_guid = jq.plan_guid;
                            a.temp_guid = item.temp_guid;
                            newpred.Add(a);
                        }
                    }
                }
            }

            sSQL.Clear();
            sSQL.AppendLine("delete from PLN_TASKPRED where plan_guid='" + A2NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            DBService.Context.Insert(newpred);
        }
        public static void GetA1WBSHsNew()//将存在构件的wbs读取为作业
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  " + //wbs层级
            "                                       and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1 ").ToDataTable();//A1标段是001001001，A2标段是001001002

            //此方法处理将ebs复制到wbs中的方法
            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 ) 
                 and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1
                 union all 
				 select 2 as XH,A.wbs_id,A.wbs_name,A.parent_wbs_id from view_bim_wbs A where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  and 
                A.wbs_id like '001001001%' and A.wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id and is_deleted<>1))   ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (Zy.ContainsKey(item["wbs_id"].ToString()))
                {
                    continue;
                }
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = Proj_id;
                WBS.plan_id = A1Plan_id;
                WBS.proj_guid = NowProj_guid;//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = A1parent_wbs_guid;//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = A1NowPlan_guid;//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (DataRow data in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                string item = data["wbs_id"].ToString();
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !data["XH"].ToString().Equals("2"))
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                    lN_Task.task_name = data["wbs_name"].ToString();
                }
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.task_name = "基础作业";
                    //lN_Task.TempTask_code = data["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = Proj_id;
                lN_Task.plan_id = A1Plan_id;
                lN_Task.proj_guid = NowProj_guid;//需要获取对应标段
                lN_Task.plan_guid = A1NowPlan_guid;//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                //lN_Task.task_name = data["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);

            sSQL.Clear();
            sSQL.AppendLine("update PLN_PROJWBS set parent_wbs_id =(select wbs_id from PLN_PROJWBS B where PLN_PROJWBS.parent_wbs_guid=B.wbs_guid) where plan_guid='"+ A1NowPlan_guid + "' ");
            dbSQL.doSQL(sSQL.ToString());
        }

        public static void GetA2WBSHsNew()//将存在构件的wbs读取为作业
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs where is_deleted<>1)  " + //wbs层级
            "                                       and wbs_id like '001001002%' and wbs_id<> '001001002' and is_deleted<>1").ToDataTable();//A1标段是001001001，A2标段是001001002

            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 ) 
                 and wbs_id like '001001002%' and wbs_id<> '001001002'and is_deleted<>1
                 union all 
                 select 2 as XH,A.wbs_id,A.wbs_name,A.parent_wbs_id from view_bim_wbs A where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  and 
                A.wbs_id like '001001002%' and A.wbs_id<> '001001002' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id and is_deleted<>1))   ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (Zy.ContainsKey(item["wbs_id"].ToString()))
                {
                    continue;
                }
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = Proj_id;
                WBS.plan_id = A2Plan_id;
                WBS.proj_guid = NowProj_guid;//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = A2parent_wbs_guid;//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = A2NowPlan_guid;//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (DataRow data in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                string item = data["wbs_id"].ToString();
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !data["XH"].ToString().Equals("2"))
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                    lN_Task.task_name = data["wbs_name"].ToString();
                }
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.task_name = "基础作业";
                    //lN_Task.TempTask_code = data["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = Proj_id;
                lN_Task.plan_id = A2Plan_id;
                lN_Task.proj_guid = NowProj_guid;//需要获取对应标段
                lN_Task.plan_guid = A2NowPlan_guid;//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                //lN_Task.task_name = data["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);

            sSQL.Clear();
            sSQL.AppendLine("update PLN_PROJWBS set parent_wbs_id =(select wbs_id from PLN_PROJWBS B where PLN_PROJWBS.parent_wbs_guid=B.wbs_guid) where plan_guid='"+ A2NowPlan_guid + "' ");
            dbSQL.doSQL(sSQL.ToString());
        }

        public static void GetA1WBSHsTest()//插入A1标段的WBS和作业(此方法处理将ebs复制到wbs中)
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if(PLN_taskproc.Tables[0].Rows.Count>0)
            {
                temp =int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp+1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 )  " + //wbs层级
            "                                       and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1 ").ToDataTable();//A1标段是001001001，A2标段是001001002

            //此方法处理将ebs复制到wbs中的方法
            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id,'00000000-0000-0000-0000-000000000000' as ebs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 ) 
                 and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1
                 union all 
                 select 2 as XH,E.wbs_id,A.ebs_name,E.parent_wbs_id,ebs_id from view_bim_ebs A 
								 left join view_bim_wbs E on E.wbs_id=A.wbs_id
								 where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and 
                A.wbs_id like '001001001%' and A.wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id))  ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);//wbs_id是唯一的，所以每一个wbs_id对应一个guid
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (Zy.ContainsKey(item["wbs_id"].ToString()))//如果已经存在相同的wbs_id则跳过
                {
                    continue;
                }
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 8;
                WBS.plan_id = 1030;
                WBS.proj_guid = "8ba0045f-ba50-4b00-898d-50337ff12182";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "6a1279e8-47d8-4cdc-91bc-af4db58b3901";//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "e223174f-c611-d125-a275-b3ca7d70eb87";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (DataRow data in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                string item = data["wbs_id"].ToString();
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.TempTask_code = data["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 8;
                lN_Task.plan_id = 1030;
                lN_Task.proj_guid = "8ba0045f-ba50-4b00-898d-50337ff12182";//需要获取对应标段
                lN_Task.plan_guid = "e223174f-c611-d125-a275-b3ca7d70eb87";//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = data["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);

            sSQL.Clear();
            sSQL.AppendLine("update PLN_PROJWBS set parent_wbs_id =(select wbs_id from PLN_PROJWBS B where PLN_PROJWBS.parent_wbs_guid=B.wbs_guid) where plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87' ");
            dbSQL.doSQL(sSQL.ToString());
        }

        public static void GetA2WBSHsTest()//插入A2标段的WBS和作业(此方法处理将ebs复制到wbs中)
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs where is_deleted<>1)  " + //wbs层级
            "                                       and wbs_id like '001001002%' and wbs_id<> '001001002' and is_deleted<>1").ToDataTable();//A1标段是001001001，A2标段是001001002

            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id,'00000000-0000-0000-0000-000000000000' as ebs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1 ) 
                 and wbs_id like '001001002%' and wbs_id<> '001001002'and is_deleted<>1
                 union all 
                 select 2 as XH,E.wbs_id,A.ebs_name,E.parent_wbs_id,ebs_id from view_bim_ebs A 
								 left join view_bim_wbs E on E.wbs_id=A.wbs_id
								 where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and 
                A.wbs_id like '001001002%' and A.wbs_id<> '001001002' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id))  ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (Zy.ContainsKey(item["wbs_id"].ToString()))
                {
                    continue;
                }
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 8;
                WBS.plan_id = 1031;
                WBS.proj_guid = "8ba0045f-ba50-4b00-898d-50337ff12182";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "b183907b-e261-428b-9c42-a3f8a6ad20ce";//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "c1e9edbb-358b-3585-aefd-a270549628af";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (DataRow data in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                string item = data["wbs_id"].ToString();
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.TempTask_code = data["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 8;
                lN_Task.plan_id = 1031;
                lN_Task.proj_guid = "8ba0045f-ba50-4b00-898d-50337ff12182";//需要获取对应标段
                lN_Task.plan_guid = "c1e9edbb-358b-3585-aefd-a270549628af";//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !data["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = data["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);

            sSQL.Clear();
            sSQL.AppendLine("update PLN_PROJWBS set parent_wbs_id =(select wbs_id from PLN_PROJWBS B where PLN_PROJWBS.parent_wbs_guid=B.wbs_guid) where plan_guid='c1e9edbb-358b-3585-aefd-a270549628af' ");
            dbSQL.doSQL(sSQL.ToString());
        }

        public static void GetorUpdateA1WBSHsTest()//更新或插入A1标段的WBS和作业(此方法处理将ebs复制到wbs中)
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  " + //wbs层级
            "                                       and wbs_id like '001001001%' and wbs_id<> '001001001'").ToDataTable();//A1标段是001001001，A2标段是001001002

            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id,'00000000-0000-0000-0000-000000000000' as ebs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs ) 
                 and wbs_id like '001001001%' and wbs_id<> '001001001'
                 union all 
                 select 2 as XH,E.wbs_id,A.ebs_name,E.parent_wbs_id,ebs_id from view_bim_ebs A 
								 left join view_bim_wbs E on E.wbs_id=A.wbs_id
								 where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and 
                A.wbs_id like '001001001%' and A.wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id))  ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            DataTable projwbs = DBService.Context.FromSql("select * from pln_projwbs where plan_guid='c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74'").ToDataTable();

            DataTable pln_task = DBService.Context.FromSql("select * from pln_task where plan_guid='c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74'").ToDataTable();


            Dictionary<string, Guid> NewData = new Dictionary<string, Guid>();
            Dictionary<string, DataRow> NewDataDx = new Dictionary<string, DataRow>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (projwbs.Select(" wbs_short_name='" + dataTable.Rows[i]["wbs_id"].ToString().Trim() + "'").Length == 0)
                {
                    Guid guid = Guid.NewGuid();
                    NewData.Add(dataTable.Rows[i]["wbs_id"].ToString().Trim(), guid);
                    NewDataDx.Add(dataTable.Rows[i]["wbs_id"].ToString().Trim(), dataTable.Rows[i]);
                }
            }

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (projwbs.Select(" wbs_short_name='" + item["wbs_id"].ToString() + "'").Length == 0)
                    Oteher.Add(item["wbs_id"].ToString(), guid);
                else
                    Oteher.Add(item["wbs_id"].ToString(), Guid.Parse(projwbs.Select(" wbs_short_name='" + item["wbs_id"].ToString() + "'")[0]["wbs_guid"].ToString()));
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }

            //查出新增作业
            for (int i = 0; i < dtZy.Rows.Count; i++)
            {
                if (dtZy.Rows[i]["XH"].ToString().Trim().Equals("1") && pln_task.Select(" task_code='" + dtZy.Rows[i]["wbs_id"].ToString().Trim() + "'").Length == 0)
                {
                    Guid guid = Guid.NewGuid();
                    if (Zy.ContainsKey(dtZy.Rows[i]["wbs_id"].ToString().Trim()))
                    {
                        continue;
                    }
                    Zy.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), guid);
                    ZyDx.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), dtZy.Rows[i]);
                }
                else if (dtZy.Rows[i]["XH"].ToString().Trim().Equals("2") && pln_task.Select(" TempTask_code='" + dtZy.Rows[i]["ebs_id"].ToString().Trim() + "'").Length == 0)
                {
                    Guid guid = Guid.NewGuid();
                    if (Zy.ContainsKey(dtZy.Rows[i]["wbs_id"].ToString().Trim()))
                    {
                        continue;
                    }
                    Zy.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), guid);
                    ZyDx.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), dtZy.Rows[i]);
                }
            }

            //将现有的wbs的关系存入对象wbsid中
            foreach (DataRow item in projwbs.Rows)
            {
                if (item["wbs_short_name"].ToString() != "A1")
                    wbsid.Add(item["wbs_short_name"].ToString(), int.Parse(item["wbs_id"].ToString()));
            }

            //循环结构
            foreach (string item in NewDataDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 7;
                WBS.plan_id = 1026;
                WBS.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "ae5be28e-60c2-4cca-8661-4d7de9bc1e1e";//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (string item in ZyDx.Keys)
            {
                Guid guid = Guid.NewGuid();
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !ZyDx[item]["XH"].ToString().Equals("2"))
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.TempTask_code = ZyDx[item]["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 7;
                lN_Task.plan_id = 1026;
                lN_Task.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";//需要获取对应标段
                lN_Task.plan_guid = "c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74";//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001") && !ZyDx[item]["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = ZyDx[item]["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);
        }

        public static void GetorUpdateA2WBSHsTest()//更新或插入A2标段的WBS和作业(此方法处理将ebs复制到wbs中)
        {
            int temp = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from PLN_PROJWBS ");
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
            }

            sSQL.Clear();
            sSQL.AppendLine("select max(wbs_id) wbs_id from pln_task ");
            PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            if (PLN_taskproc.Tables[0].Rows.Count > 0)
            {
                if (temp < int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString()))
                {
                    temp = int.Parse(PLN_taskproc.Tables[0].Rows[0]["wbs_id"].ToString());
                }
            }
            wbs = temp + 1;

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  " + //wbs层级
            "                                       and wbs_id like '001001002%' and wbs_id<> '001001002'").ToDataTable();//A1标段是001001001，A2标段是001001002

            string sql = @"select 1 as XH,wbs_id,wbs_name,parent_wbs_id,'00000000-0000-0000-0000-000000000000' as ebs_id from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs ) 
                 and wbs_id like '001001002%' and wbs_id<> '001001002'
                 union all 
                 select 2 as XH,E.wbs_id,A.ebs_name,E.parent_wbs_id,ebs_id from view_bim_ebs A 
								 left join view_bim_wbs E on E.wbs_id=A.wbs_id
								 where A.wbs_id in(select wbs_id from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and 
                A.wbs_id like '001001002%' and A.wbs_id<> '001001002' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id))  ";

            DataTable dtZy = DBService.My.FromSql(sql).ToDataTable();

            DataTable projwbs = DBService.Context.FromSql("select * from pln_projwbs where plan_guid='8fafab0e-da05-607e-20d7-0faa24076e90'").ToDataTable();

            DataTable pln_task = DBService.Context.FromSql("select * from pln_task where plan_guid='8fafab0e-da05-607e-20d7-0faa24076e90'").ToDataTable();


            Dictionary<string, Guid> NewData = new Dictionary<string, Guid>();
            Dictionary<string, DataRow> NewDataDx = new Dictionary<string, DataRow>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (projwbs.Select(" wbs_short_name='" + dataTable.Rows[i]["wbs_id"].ToString().Trim() + "'").Length==0)
                {
                    Guid guid = Guid.NewGuid();
                    NewData.Add(dataTable.Rows[i]["wbs_id"].ToString().Trim(), guid);
                    NewDataDx.Add(dataTable.Rows[i]["wbs_id"].ToString().Trim(), dataTable.Rows[i]);
                }
            }            

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (projwbs.Select(" wbs_short_name='" + item["wbs_id"].ToString() + "'").Length == 0)
                    Oteher.Add(item["wbs_id"].ToString(), guid);
                else
                    Oteher.Add(item["wbs_id"].ToString(),Guid.Parse(projwbs.Select(" wbs_short_name='" + item["wbs_id"].ToString() + "'")[0]["wbs_guid"].ToString()));
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }

            //查出新增作业
            for (int i = 0; i < dtZy.Rows.Count; i++)
            {
                if (dtZy.Rows[i]["XH"].ToString().Trim().Equals("1") && pln_task.Select(" task_code='" + dtZy.Rows[i]["wbs_id"].ToString().Trim() + "'").Length == 0)
                {
                    Guid guid = Guid.NewGuid();
                    if (Zy.ContainsKey(dtZy.Rows[i]["wbs_id"].ToString().Trim()))
                    {
                        continue;
                    }
                    Zy.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), guid);
                    ZyDx.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), dtZy.Rows[i]);
                }
                else if (dtZy.Rows[i]["XH"].ToString().Trim().Equals("2") && pln_task.Select(" TempTask_code='" + dtZy.Rows[i]["ebs_id"].ToString().Trim() + "'").Length == 0)
                {
                    Guid guid = Guid.NewGuid();
                    if (Zy.ContainsKey(dtZy.Rows[i]["wbs_id"].ToString().Trim()))
                    {
                        continue;
                    }
                    Zy.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), guid);
                    ZyDx.Add(dtZy.Rows[i]["wbs_id"].ToString().Trim(), dtZy.Rows[i]);
                }
            }

            //将现有的wbs的关系存入对象wbsid中
            foreach (DataRow item in projwbs.Rows)
            {
                if(item["wbs_short_name"].ToString()!="A2")
                wbsid.Add(item["wbs_short_name"].ToString(), int.Parse(item["wbs_id"].ToString()));
            }

            //循环结构
            foreach (string item in NewDataDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 7;
                WBS.plan_id = 1027;
                WBS.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "df77525b-b170-4992-9126-c6fa21585acc";//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "8fafab0e-da05-607e-20d7-0faa24076e90";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }            

            foreach (string item in ZyDx.Keys)
            {
                Guid guid = Guid.NewGuid();
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = guid.ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !ZyDx[item]["XH"].ToString().Equals("2"))
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                else
                {
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                    lN_Task.TempTask_code = ZyDx[item]["ebs_id"].ToString();
                }
                //taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 7;
                lN_Task.plan_id = 1027;
                lN_Task.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";//需要获取对应标段
                lN_Task.plan_guid = "8fafab0e-da05-607e-20d7-0faa24076e90";//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002") && !ZyDx[item]["XH"].ToString().Equals("2"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = ZyDx[item]["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);
        }

        public static void GetA1WBSHs()//插入A1标段的WBS和作业
        {

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  " + //wbs层级
                "                                       and wbs_id like '001001001%' and wbs_id<> '001001001'").ToDataTable();//A1标段是001001001，A2标段是001001002

            DataTable dtZy = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs where is_deleted<>1  ) " +
                " and wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1 " +
                " union all " +
                " select * from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and " +
                " wbs_id like '001001001%' and wbs_id<> '001001001' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id)  ").ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 6;
                WBS.plan_id = 1020;
                WBS.proj_guid = "a56eaa9f-d638-456c-b240-2cb92a6bfbfd";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "b566d014-3741-4c35-aa92-a9f881726873";//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "72d65f18-6bb8-cf61-997e-3c0c1ab2edb4";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (string item in ZyDx.Keys)
            {
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = Zy[item].ToString();
                lN_Task.task_id = task;
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001"))
                    lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                else
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 6;
                lN_Task.plan_id = 1020;
                lN_Task.proj_guid = "a56eaa9f-d638-456c-b240-2cb92a6bfbfd";//需要获取对应标段
                lN_Task.plan_guid = "72d65f18-6bb8-cf61-997e-3c0c1ab2edb4";//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001001"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = ZyDx[item]["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);
        }


        public static void GetA2WBSHs()//插入A2标段的WBS和作业
        {

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  " + //wbs层级
                "                                       and wbs_id like '001001002%' and wbs_id<> '001001002'").ToDataTable();//A1标段是001001001，A2标段是001001002
            DataTable dtZy = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs ) " +
                " and wbs_id like '001001002%' and wbs_id<> '001001002'" +
                " union all " +
                " select * from view_bim_wbs A  where wbs_id in(select parent_wbs_id from view_bim_wbs )  and wbs_id like '001001002%' and wbs_id<> '001001002' and exists(select 1 from view_bim_ebs B where A.wbs_id=B.wbs_id)  ").ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 6;
                WBS.plan_id = 1021;
                WBS.proj_guid = "a56eaa9f-d638-456c-b240-2cb92a6bfbfd";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "ade1ba88-718d-4333-89d2-cf28caa9b1f9";//将parent_wbs_guid置为顶层的wbs_guid
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "344c3576-706d-51c5-939e-9f67897fdbc8";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (string item in ZyDx.Keys)
            {
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = Zy[item].ToString();
                lN_Task.task_id = task;
                if(!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002"))
                  lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                else
                    lN_Task.wbs_id = wbsid[ZyDx[item]["wbs_id"].ToString()];
                taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 6;
                lN_Task.plan_id = 1021;
                lN_Task.proj_guid = "a56eaa9f-d638-456c-b240-2cb92a6bfbfd";//需要获取对应标段
                lN_Task.plan_guid = "344c3576-706d-51c5-939e-9f67897fdbc8";//需要获取对应标段
                if (!ZyDx[item]["parent_wbs_id"].ToString().Equals("001001002"))
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                else
                    lN_Task.wbs_guid = Oteher[ZyDx[item]["wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = ZyDx[item]["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            DBService.Context.Insert(lN_PROJWBs);
            DBService.Context.Insert(lN_Tasks);
        }


        //华设
        public static void GetWBSHs()
        {

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  " + //wbs层级
                "                                       and wbs_id like '001001002%' and wbs_id<> '001001002'").ToDataTable();//A1标段是001001001，A2标段是001001002
            DataTable dtZy = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id not in(select parent_wbs_id from view_bim_wbs ) " +
                " and wbs_id like '001001002%' and wbs_id<> '001001002' ").ToDataTable();

            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            foreach (DataRow item in dtZy.Rows)
            {
                Guid guid = Guid.NewGuid();
                Zy.Add(item["wbs_id"].ToString(), guid);
                ZyDx.Add(item["wbs_id"].ToString(), item);
            }

            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item,wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 5;
                WBS.plan_id = 1018;
                WBS.proj_guid = "1cc767c9-ec0f-403c-ad37-616487cc5139";//需要获取对应标段
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name =OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = "589b244d-d61b-4b96-893e-d0b7ebdb073f";//将parent_wbs_guid置为顶层的wbs_guid
                }
              
                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "7c099591-6068-371f-218f-5ac51a3aa531";//需要获取对应标段
                lN_PROJWBs.Add(WBS);
                wbs++;
            }

            foreach (string item in ZyDx.Keys)
            {
                PLN_task lN_Task = new PLN_task();
                lN_Task.task_guid = Zy[item].ToString();
                lN_Task.task_id = task;
                lN_Task.wbs_id = wbsid[ZyDx[item]["parent_wbs_id"].ToString()];
                taskid.Add(Zy[item].ToString(), task);
                lN_Task.proj_id = 5;
                lN_Task.plan_id = 1018;
                lN_Task.proj_guid = "1cc767c9-ec0f-403c-ad37-616487cc5139";//需要获取对应标段
                lN_Task.plan_guid = "7c099591-6068-371f-218f-5ac51a3aa531";//需要获取对应标段
                lN_Task.wbs_guid = Oteher[ZyDx[item]["parent_wbs_id"].ToString()].ToString();
                lN_Task.clndr_id = 0;                
                lN_Task.parent_guid = "00000000-0000-0000-0000-000000000000";
                lN_Task.rev_fdbk_flag = "N";
                lN_Task.lock_plan_flag = "N";
                lN_Task.complete_pct_type = "CP_Drtn";
                lN_Task.task_type = "TT_Task";
                lN_Task.status_code = "TK_NotStart";
                lN_Task.task_code = ZyDx[item]["wbs_id"].ToString();
                lN_Task.task_name = ZyDx[item]["wbs_name"].ToString();
                lN_Task.guid = Zy[item].ToString();
                lN_Task.clndr_guid = "348979ac-95d1-421b-9f64-c787751bc7d3";
                lN_Tasks.Add(lN_Task);
                task++;
            }
            //DBService.Context.Insert(lN_PROJWBs);
            //DBService.Context.Insert(lN_Tasks);
        }

        //修复wbs结构混乱的方法
        public static void ChangeLevelZY()
        {
            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  and wbs_id like '001001001%' and wbs_id<> '001001001'").ToDataTable();

            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640'").ToList<PLN_task>();
            List<PLN_PROJWBS> wbss = DBService.Context.FromSql("select *from  PLN_PROJWBS  where wbs_short_name like '001001001%'and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640'").ToList<PLN_PROJWBS>();
            List<PLN_task> up = new List<PLN_task>();

            //修改作业层级
            foreach (PLN_task item in task)
            {
                string code = item.task_code.Substring(0, item.task_code.Length - 3);
                if (wbss.Find(z => z.wbs_short_name == code) != null)
                {
                    PLN_PROJWBS wbsitem = wbss.Find(z => z.wbs_short_name == code);
                    item.wbs_guid = wbsitem.wbs_guid;
                    item.wbs_id = wbsitem.wbs_id;
                    up.Add(item);
                }
            }
            //DBService.Context.Insert(Ins);
            DBService.Context.Update<PLN_task>(up);

        }


        public static void updateNowTask()
        {
            List<PLN_task> oldtask = DBService.Old.FromSql("Select * From  PLN_task  Where   (0=0)  and plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87' and 1=1  Order By  task_code asc").ToList<PLN_task>();
            List<PLN_task> Nowtask = DBService.Context.FromSql("Select * From  PLN_task  Where   (0=0)  and plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87' and 1=1  Order By  task_code asc").ToList<PLN_task>();
            List<PLN_task> up = new List<PLN_task>();

            foreach (PLN_task item in Nowtask)
            {
                PLN_task pLN_Task = oldtask.Find(z => z.task_guid == item.task_guid);
                if (pLN_Task != null)
                {
                    if(pLN_Task.act_start_date!=null || pLN_Task.act_end_date!=null || pLN_Task.complete_pct!=0)
                    {
                        item.status_code = pLN_Task.status_code;
                        item.act_start_date = pLN_Task.act_start_date;
                        item.act_end_date = pLN_Task.act_end_date;
                        item.complete_pct = pLN_Task.complete_pct;
                        up.Add(item);
                    }
                    
                }
            }
            DBService.Context.Update<PLN_task>(up);
        }


        public static void UpTaskBCWS()
        {
            List<PLN_PROJWBS> oldtask = DBService.Old.FromSql("Select * From  PLN_PROJWBS  Where   (0=0)  and (plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87') and 1=1  Order By  wbs_short_name asc ").ToList<PLN_PROJWBS>();
            List<PLN_PROJWBS> Nowtask = DBService.Context.FromSql("Select * From  PLN_PROJWBS  Where   (0=0)  and (plan_guid='e223174f-c611-d125-a275-b3ca7d70eb87') and 1=1  Order By  wbs_short_name asc").ToList<PLN_PROJWBS>();
            List<PLN_PROJWBS> up = new List<PLN_PROJWBS>();

            foreach (PLN_PROJWBS item in Nowtask)
            {
                PLN_PROJWBS PROJWBS = oldtask.Find(z => z.wbs_short_name == item.wbs_short_name);
                if (PROJWBS != null)
                {
                    if (PROJWBS.complete_pct != 0)
                    {
                        item.complete_pct = PROJWBS.complete_pct;
                        up.Add(item);
                    }

                }
            }
            DBService.Context.Update<PLN_PROJWBS>(up);
        }

        //修复wbs结构混乱的方法
        public static void ChangeLevelWBS()
        {
            List<PLN_PROJWBS> wbss = DBService.Context.FromSql("select * from PLN_PROJWBS where plan_guid='c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74' and wbs_short_name like '001001001%'").ToList<PLN_PROJWBS>();
            List<PLN_PROJWBS> up = new List<PLN_PROJWBS>();
            foreach (PLN_PROJWBS item in wbss)
            {
                string code = item.wbs_short_name.Substring(0, item.wbs_short_name.Length - 3);
                if ((wbss.Find(z => z.wbs_short_name == code) != null) && item.parent_wbs_id ==null)
                {
                    PLN_PROJWBS wbsitem = wbss.Find(z => z.wbs_short_name == code);
                    item.parent_wbs_id = wbsitem.wbs_id;
                    up.Add(item);
                }
            }
            DBService.Context.Update<PLN_PROJWBS>(up);

        }




        //处理作业层级乱掉

        public static void ChangeLevel()
        {
            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  and wbs_id like '001001001%' and wbs_id<> '001001001'").ToDataTable();

            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='67ceeb00-1c27-409d-9825-c9f048615b3e'").ToList<PLN_task>();
            List<PLN_PROJWBS> wbss = DBService.Context.FromSql("select *from  PLN_PROJWBS  where wbs_short_name like '001001001%'and proj_guid='67ceeb00-1c27-409d-9825-c9f048615b3e'").ToList<PLN_PROJWBS>();
            List<PLN_task> up = new List<PLN_task>();

            List<PLN_PROJWBS> Ins = new List<PLN_PROJWBS>();
            //生成GUID
            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                Oteher.Add(item["wbs_id"].ToString(), guid);
                OteherDx.Add(item["wbs_id"].ToString(), item);
            }
            //循环结构
            foreach (string item in OteherDx.Keys)
            {
                PLN_PROJWBS WBS = new PLN_PROJWBS();
                WBS.wbs_id = wbs;
                wbsid.Add(item, wbs);
                WBS.wbs_guid = Oteher[item].ToString();
                WBS.proj_id = 8;
                WBS.proj_guid = "67ceeb00-1c27-409d-9825-c9f048615b3e";
                WBS.obs_id = 0;
                WBS.seq_num = 10;
                WBS.proj_node_flag = "0";
                WBS.sum_data_flag = "N";
                WBS.status_code = "0";
                WBS.wbs_short_name = item;
                WBS.wbs_name = OteherDx[item]["wbs_name"].ToString();
                WBS.phase_id = 0;

                if (Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                    WBS.parent_wbs_guid = Oteher[OteherDx[item]["parent_wbs_id"].ToString()].ToString();
                }
                else if (!Oteher.ContainsKey(OteherDx[item]["parent_wbs_id"].ToString()))
                {
                //    WBS.parent_wbs_guid = "08130fa1-e750-4cbc-94e8-5fa682fda260";
                }

                WBS.guid = Oteher[item].ToString();
                WBS.update_date = DateTime.Now;
                WBS.plan_guid = "ddcaf65e-fee0-c39c-c70e-59a76c218481";
                WBS.plan_id = 1024;
                Ins.Add(WBS);
                wbs++;
            }

            //修改作业层级
            foreach (PLN_task item in task)
            {
                string code = item.task_code.Substring(0,item.task_code.Length-3);
                if (Ins.Find(z=>z.wbs_short_name==code)!=null)
                {
                    PLN_PROJWBS wbsitem = Ins.Find(z => z.wbs_short_name == code);
                    item.wbs_guid = wbsitem.wbs_guid;
                    item.wbs_id = wbsitem.wbs_id;
                    up.Add(item);
                }
            }
            DBService.Context.Insert(Ins);
            DBService.Context.Update<PLN_task>(up);

        }

        public static void GetGCLQD()//刷新工程量清单
        {
            List<NPS_BOQ> nPS_BOQs = new List<NPS_BOQ>();
            MySQLDataBase dbYSJ = new MySQLDataBase();
            StringBuilder sSQL3 = new StringBuilder();

            DataTable QgDt = DBService.My.FromSql("select * from view_bim_qingdam_map").ToDataTable();
            DataTable view_bim_qingdan = DBService.My.FromSql("select S_XIANGMMC,B.S_QDBH as S_QDBH1,A.S_QDBH as S_QDBH ,F_DANJ,F_SHUL,F_JINE,F_FHDJ,F_FHSL,F_FHJE,A.S_LISTINGID as S_LISTINGID1  from view_bim_qingdan A " +
                "left join (select S_QDBH,S_LISTINGID from view_bim_qingdan where S_LISTINGID=substring(S_LISTINGID,1,12)) B on substring(A.S_LISTINGID,1,12)=B.S_LISTINGID ").ToDataTable();
            List<PLN_taskproc> task = DBService.Context.FromSql("select * from pln_taskproc where proj_id=10 and plan_guid<>'df89f1d2-8210-4edd-bbf7-e60ee3be2a11'and plan_guid='3d82fc97-0ca9-3409-9e03-b11e565f5836' ").ToList<PLN_taskproc>();
            DataTable dataTable = DBService.My.FromSql("select ebs_id from view_bim_ebs group by ebs_id ").ToDataTable();

            //List<NPS_BOQ> NPS_BOQList = DBService.Context.FromSql("select * from NPS_BOQ left join pln_taskproc on fid=proc_guid where plan_guid<>'df89f1d2-8210-4edd-bbf7-e60ee3be2a11' ").ToList<NPS_BOQ>();

            foreach (DataRow item in dataTable.Rows)
            {
                string proc_guid = "00000000-0000-0000-0000-00000000000A";
                string ebs_id = item["ebs_id"].ToString();
                DateTime PlanSatrt=DateTime.Now;
                DateTime PlanEnd=DateTime.Now;
                PLN_taskproc lN_Taskproc = task.Find(X => X.proc_code == item["ebs_id"].ToString().Trim());
                if (lN_Taskproc != null)
                {
                    proc_guid = lN_Taskproc.proc_guid;
                    //if (DateTime.TryParse(lN_Taskproc.PLN_target_start_date.ToString(), out PlanSatrt))
                    //{ }
                    //else
                    //    PlanSatrt = DateTime.Now;
                    //if (DateTime.TryParse(lN_Taskproc.PLN_target_start_date.ToString(), out PlanEnd))
                    //{ }
                    //else
                    //    PlanSatrt = DateTime.Now;
                    //        PlanSatrt = DateTime.Parse(lN_Taskproc.PLN_target_start_date.ToString());
                    //PlanEnd = DateTime.Parse(lN_Taskproc.PLN_target_end_date.ToString());

                    //NPS_BOQ _BOQ = NPS_BOQList.Find(x => x.FID == Guid.Parse(proc_guid));
                    //if (_BOQ == null)
                    //{
                        sSQL3.Clear();
                        sSQL3.AppendLine("select sum(F_COUNT) as F_COUNT,sum(F_MONEY) as F_MONEY,s_listingid from view_bim_qingdam_map B where B.S_ISBN like @S_ISBN group by s_listingid");
                        dbYSJ.addParam("S_ISBN", ebs_id + "%");
                        DataSet view_bim_qingdam_map = dbYSJ.getDataSet(sSQL3.ToString());
                        if (view_bim_qingdam_map.Tables[0].Rows.Count > 0)
                        {
                            for (int y = 0; y < view_bim_qingdam_map.Tables[0].Rows.Count; y++)
                            {
                                string s_listingid = view_bim_qingdam_map.Tables[0].Rows[y]["s_listingid"].ToString();

                                foreach (DataRow ZZitem in view_bim_qingdan.Rows)
                                {
                                    if (ZZitem["S_LISTINGID1"].ToString().Equals(s_listingid))
                                    {
                                        NPS_BOQ PS_BOQ = new NPS_BOQ();
                                        PS_BOQ.ID = Guid.NewGuid();
                                        PS_BOQ.ListingNum = float.Parse(view_bim_qingdam_map.Tables[0].Rows[y]["F_COUNT"].ToString());
                                        PS_BOQ.ListingPrice = float.Parse(view_bim_qingdam_map.Tables[0].Rows[y]["F_MONEY"].ToString());
                                        PS_BOQ.ListingCode = ZZitem["S_QDBH"].ToString();
                                        PS_BOQ.ListingName = ZZitem["S_XIANGMMC"].ToString();
                                        PS_BOQ.Amount = float.Parse(ZZitem["F_DANJ"].ToString());
                                        PS_BOQ.S_ISBN = ebs_id;
                                        PS_BOQ.TableName = "NPS_BOQ";
                                        PS_BOQ.BizAreaId = Guid.Parse("00000000-0000-0000-0000-00000000000A");
                                        PS_BOQ.Sequ = 0;
                                        PS_BOQ.Status = 0;
                                        PS_BOQ.RegHumId = Guid.Parse("AD000000-0000-0000-0000-000000000000");
                                        PS_BOQ.RegHumName = "系统管理员";
                                        PS_BOQ.RegDate = DateTime.Now;
                                        PS_BOQ.RegPosiId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PS_BOQ.RegDeptId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PS_BOQ.RecycleHumId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PS_BOQ.UpdHumId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PS_BOQ.UpdHumName = "系统管理员";
                                        PS_BOQ.UpdDate = DateTime.Now;
                                        PS_BOQ.ApprHumId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                                        PS_BOQ.Chapter = ZZitem["S_QDBH1"].ToString();
                                        PS_BOQ.FID = Guid.Parse(proc_guid);
                                        //PS_BOQ.PlanSatrt = DateTime.Parse(PlanSatrt.ToString());
                                        //PS_BOQ.PlanEnd = DateTime.Parse(PlanEnd.ToString());
                                        nPS_BOQs.Add(PS_BOQ);
                                        break;
                                    }
                                }
                            }

                        }
                    //}
                }


            }

            DBService.Context.Insert(nPS_BOQs);
        }

        public static void GetGX()//刷新工序
        {
            List<PS_PLN_TaskProc_Sub> PS_PLN_TaskProc_Sub = new List<PS_PLN_TaskProc_Sub>();
            List<PS_PLN_TaskProc_Sub> DelSub = new List<PS_PLN_TaskProc_Sub>();
            List<PS_PLN_TaskProc_Sub> CurSub = DBService.Context.FromSql("select * from PS_PLN_TaskProc_Sub where 1=1 ").ToList<PS_PLN_TaskProc_Sub>();
            List<PLN_taskproc> task = DBService.Context.FromSql("select * from pln_taskproc where proj_id='"+ Proj_id + "' ").ToList<PLN_taskproc>();
            DataTable dataTable = DBService.My.FromSql("select distinct A.ebs_type_id,ebs_id,A.ebs_type_name,process_id,process_step_name,process_sort,process_remark " +
                                                    " from  view_bim_ebs B,view_bim_inspection_process A   where A.is_deleted<>'1' and A.ebs_type_id=B.ebs_type_id and B.is_deleted<>'1' ").ToDataTable();
            int seq_num = 0;
            foreach (DataRow item in dataTable.Rows)
            {
                PS_PLN_TaskProc_Sub TempSub = CurSub.Find(z => z.ProcSub_Code == item["ebs_id"].ToString().Trim() + item["process_id"].ToString());
                if (TempSub == null)
                {
                    PLN_taskproc taskitem = task.Find(z => z.proc_code == item["ebs_id"].ToString().Trim());
                    if (taskitem != null)
                    {
                        List<PS_PLN_TaskProc_Sub> TempSub1 = CurSub.FindAll(z => z.proc_guid == taskitem.proc_guid);
                        if (TempSub1 != null)
                        {
                            foreach (PS_PLN_TaskProc_Sub tem in TempSub1)
                            {
                                PS_PLN_TaskProc_Sub TaskProc_Sub = DelSub.Find(z => z.ProcSub_guid == tem.ProcSub_guid);
                                if (TaskProc_Sub != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    DelSub.Add(tem);
                                }
                            }

                        }
                        if (PS_PLN_TaskProc_Sub.Find(z => z.ProcSub_Code == item["ebs_id"].ToString().Trim() + item["process_id"].ToString()) == null)
                        {
                            Guid guid = Guid.NewGuid();
                            PS_PLN_TaskProc_Sub lN_Taskproc = new PS_PLN_TaskProc_Sub();
                            lN_Taskproc.ProcSub_guid = guid.ToString();
                            lN_Taskproc.ProcSub_Code = item["ebs_id"].ToString().Trim() + item["process_id"].ToString();
                            lN_Taskproc.ProcSub_Name = item["process_step_name"].ToString();
                            lN_Taskproc.proj_guid = taskitem.proj_guid;
                            lN_Taskproc.plan_guid = taskitem.plan_guid;
                            lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                            lN_Taskproc.proc_guid = taskitem.proc_guid;
                            lN_Taskproc.seq_num = seq_num;
                            lN_Taskproc.est_wt = 1;
                            lN_Taskproc.complete_pct = 0;
                            lN_Taskproc.target_end_date = taskitem.PLN_target_end_date;
                            lN_Taskproc.act_end_date = taskitem.PLN_act_end_date;
                            lN_Taskproc.RegDate = DateTime.Now;
                            lN_Taskproc.RegHumName = "系统管理员";
                            lN_Taskproc.RegHumId = Guid.Parse("AD000000-0000-0000-0000-000000000000");
                            lN_Taskproc.UpdHumId = Guid.Parse("AD000000-0000-0000-0000-000000000000");
                            lN_Taskproc.UpdDate = DateTime.Now;
                            seq_num++;
                            PS_PLN_TaskProc_Sub.Add(lN_Taskproc);
                        }

                    }
                }                
            }

            for (int i = 0; i < DelSub.Count; i++)
            {
                DBService.Context.Delete<PS_PLN_TaskProc_Sub>(d => d.ProcSub_guid == DelSub[i].ProcSub_guid);
            }
            DBService.Context.Insert(PS_PLN_TaskProc_Sub);
        }
        public static void A1InsertOrUpdateGj()//新增或更新构件方法
        {

            List<PLN_taskproc> taskprocList = DBService.Context.FromSql("select * from PLN_taskproc  where plan_guid='78c2a9a4-a7bc-059c-719a-bbc0b5d22f62' ").ToList<PLN_taskproc>();//A1标段下现有的构件列表

            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='b5c3a1aa-f030-4e57-b4d9-f164660d5cd4'").ToList<PLN_task>();//A1标段下的作业列表
            DataTable dataTable = DBService.My.FromSql("select * from view_bim_ebs  where  wbs_id like '001001001%' and wbs_id<> '001001001' ").ToDataTable();//华社视图提供的A1标段下的构件

            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (!Gj.ContainsKey(item["ebs_id"].ToString()))
                {
                    Gj.Add(item["ebs_id"].ToString(), guid);
                    GjDx.Add(item["ebs_id"].ToString(), item);
                }
            }

            //foreach(PLN_taskproc taskproc in taskprocList)
            //{
            //    Boolean flag = false;
            //    foreach (DataRow item in dataTable.Rows)
            //    {
            //        if (taskproc.proc_code.Trim().Equals(item["ebs_id"].ToString()))
            //        {
            //            flag = true;
            //        }
            //    }
            //    if (!flag)//如果靖江表中的构件与华社视图中的构件没有一个相同的，则删除
            //    {
            //        //DBService.Context.Delete<PLN_taskproc>(d => d.proc_guid == taskproc.proc_guid);
            //    }
            //}
            

            foreach (DataRow item in dataTable.Rows)
            {
                PLN_taskproc PLN_taskproc = taskprocList.Find(z => z.proc_code == item["ebs_id"].ToString());
                if (PLN_taskproc != null)//如果已经存在此构件，则根据接口中的数据更新
                {
                    PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());//匹配接口中读取的wbs_id
                    {
                        if (taskitem != null)
                        {
                            PLN_taskproc.task_id = taskitem.task_id;
                            PLN_taskproc.wbs_id = taskitem.wbs_id;
                            PLN_taskproc.task_guid = taskitem.task_guid;
                            PLN_taskproc.proc_name = item["ebs_name"].ToString();

                            PLN_taskproc.plan_guid = taskitem.plan_guid;
                            PLN_taskproc.plan_id = taskitem.plan_id;
                            PLN_taskproc.wbs_guid = taskitem.wbs_guid;
                        }
                    }
                   
                }
                else//如果不存在，则直接插入即可
                {
                    PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());
                    if (taskitem != null && lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null)
                    {
                        PLN_taskproc lN_Taskproc = new PLN_taskproc();

                        lN_Taskproc.proc_id = proc;
                        lN_Taskproc.proc_code = item["ebs_id"].ToString();
                        lN_Taskproc.task_id = taskitem.task_id;
                        lN_Taskproc.wbs_id = taskitem.wbs_id;
                        lN_Taskproc.task_guid = taskitem.task_guid;
                        lN_Taskproc.proc_name = item["ebs_name"].ToString();
                        lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                        lN_Taskproc.plan_guid = taskitem.plan_guid;
                        lN_Taskproc.plan_id = taskitem.plan_id;
                        lN_Taskproc.proj_id = 4;
                        lN_Taskproc.proj_guid = "b5c3a1aa-f030-4e57-b4d9-f164660d5cd4";
                        lN_Taskproc.PLN_target_start_date = taskitem.target_start_date;
                        lN_Taskproc.PLN_target_end_date = taskitem.target_end_date;

                        lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                        lN_Taskprocs.Add(lN_Taskproc);
                        proc++;
                    }
                }
            }
            //插入构件
            DBService.Context.Update(taskprocList);
            DBService.Context.Insert(lN_Taskprocs);
        }

        public static void A2InsertOrUpdateGj()//新增或更新构件方法
        {

            List<PLN_taskproc> taskprocList = DBService.Context.FromSql("select * from PLN_taskproc  where plan_guid='037d80d5-a1a9-2893-de74-b2a68c82777e' ").ToList<PLN_taskproc>();//A2标段下现有的构件列表

            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001002%' and proj_guid='b5c3a1aa-f030-4e57-b4d9-f164660d5cd4'").ToList<PLN_task>();//A2标段下的作业列表
            DataTable dataTable = DBService.My.FromSql("select * from view_bim_ebs  where  wbs_id like '001001002%' and wbs_id<> '001001002' ").ToDataTable();//华社视图提供的A2标段下的构件

            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (!Gj.ContainsKey(item["ebs_id"].ToString()))
                {
                    Gj.Add(item["ebs_id"].ToString(), guid);
                    GjDx.Add(item["ebs_id"].ToString(), item);
                }
            }

            //foreach(PLN_taskproc taskproc in taskprocList)
            //{
            //    Boolean flag = false;
            //    foreach (DataRow item in dataTable.Rows)
            //    {
            //        if (taskproc.proc_code.Trim().Equals(item["ebs_id"].ToString()))
            //        {
            //            flag = true;
            //        }
            //    }
            //    if (!flag)//如果靖江表中的构件与华社视图中的构件没有一个相同的，则删除
            //    {
            //        //DBService.Context.Delete<PLN_taskproc>(d => d.proc_guid == taskproc.proc_guid);
            //    }
            //}


            foreach (DataRow item in dataTable.Rows)
            {
                PLN_taskproc PLN_taskproc = taskprocList.Find(z => z.proc_code == item["ebs_id"].ToString());
                if (PLN_taskproc != null)//如果已经存在此构件，则根据接口中的数据更新
                {
                    PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());//匹配接口中读取的wbs_id
                    {
                        if (taskitem != null)
                        {
                            PLN_taskproc.task_id = taskitem.task_id;
                            PLN_taskproc.wbs_id = taskitem.wbs_id;
                            PLN_taskproc.task_guid = taskitem.task_guid;
                            PLN_taskproc.proc_name = item["ebs_name"].ToString();

                            PLN_taskproc.plan_guid = taskitem.plan_guid;
                            PLN_taskproc.plan_id = taskitem.plan_id;
                            PLN_taskproc.wbs_guid = taskitem.wbs_guid;
                        }
                    }

                }
                else//如果不存在，则直接插入即可
                {
                    PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());
                    if (taskitem != null && lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null)
                    {
                        PLN_taskproc lN_Taskproc = new PLN_taskproc();

                        lN_Taskproc.proc_id = proc;
                        lN_Taskproc.proc_code = item["ebs_id"].ToString();
                        lN_Taskproc.task_id = taskitem.task_id;
                        lN_Taskproc.wbs_id = taskitem.wbs_id;
                        lN_Taskproc.task_guid = taskitem.task_guid;
                        lN_Taskproc.proc_name = item["ebs_name"].ToString();
                        lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                        lN_Taskproc.plan_guid = taskitem.plan_guid;
                        lN_Taskproc.plan_id = taskitem.plan_id;
                        lN_Taskproc.proj_id = 4;
                        lN_Taskproc.proj_guid = "b5c3a1aa-f030-4e57-b4d9-f164660d5cd4";
                        lN_Taskproc.PLN_target_start_date = taskitem.target_start_date;
                        lN_Taskproc.PLN_target_end_date = taskitem.target_end_date;

                        lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                        lN_Taskprocs.Add(lN_Taskproc);
                        proc++;
                    }
                }
            }
            //插入构件
            DBService.Context.Update(taskprocList);
            DBService.Context.Insert(lN_Taskprocs);
        }

        public static void GetA1Gj()//重新刷构件
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='"+ NowProj_guid + "'and plan_guid='" + A1NowPlan_guid + "' " +
                "and isnull(TempTask_code,' ')=' ' ").ToList<PLN_task>();//这是正常的作业
            //List<PLN_task> Untask = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='"+ NowProj_guid + "' " +
            //    "and isnull(TempTask_code,' ')<>' ' ").ToList<PLN_task>();//这是特殊的作业（即构件转换为的作业）
            //DataTable UntaskTable = DBService.My.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
            //    "and isnull(TempTask_code,' ')<>' '").ToDataTable();

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_ebs  where  wbs_id like '001001001%' and wbs_id<> '001001001' and is_deleted<>1").ToDataTable();//A1下所有的构件
            List<PLN_taskproc> OldTask = DBService.Context.FromSql("select * from  plN_taskproc where plan_guid= '" + A1NowPlan_guid + "'  ").ToList<PLN_taskproc>();//查询原来的构件表中的数据

            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (!Gj.ContainsKey(item["ebs_id"].ToString()))
                {
                    PLN_taskproc taskitem1 = OldTask.Find(z => z.proc_code == item["ebs_id"].ToString());
                    if (taskitem1 != null)
                        Gj.Add(item["ebs_id"].ToString(),Guid.Parse(taskitem1.proc_guid));
                    else
                        Gj.Add(item["ebs_id"].ToString(), guid);

                    GjDx.Add(item["ebs_id"].ToString(), item);
                }
            }

            //foreach (DataRow item in dataTable.Rows)
            //{
            //    PLN_task taskitem = Untask.Find(z => z.TempTask_code == item["ebs_id"].ToString());
            //    if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null))
            //    {
            //        PLN_taskproc lN_Taskproc = new PLN_taskproc();

            //        lN_Taskproc.proc_id = proc;
            //        lN_Taskproc.proc_code = item["ebs_id"].ToString();
            //        lN_Taskproc.task_id = taskitem.task_id;
            //        lN_Taskproc.wbs_id = taskitem.wbs_id;
            //        lN_Taskproc.task_guid = taskitem.task_guid;
            //        lN_Taskproc.proc_name = item["ebs_name"].ToString();
            //        lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

            //        lN_Taskproc.plan_guid = taskitem.plan_guid;
            //        lN_Taskproc.plan_id = taskitem.plan_id;
            //        lN_Taskproc.proj_id = Proj_id;
            //        lN_Taskproc.proj_guid = NowProj_guid;

            //        lN_Taskproc.wbs_guid = taskitem.wbs_guid;
            //        lN_Taskprocs.Add(lN_Taskproc);
            //        proc++;
            //    }

            //}

           

            foreach (DataRow item in dataTable.Rows)
            {
                PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());
                if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null))
                {
                    plN_Temptaskproc lN_Taskproc = new plN_Temptaskproc();

                    lN_Taskproc.proc_id = proc;
                    lN_Taskproc.proc_code = item["ebs_id"].ToString();
                    lN_Taskproc.task_id = taskitem.task_id;
                    lN_Taskproc.wbs_id = taskitem.wbs_id;
                    lN_Taskproc.task_guid = taskitem.task_guid;
                    lN_Taskproc.proc_name = item["ebs_name"].ToString();
                    lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                    lN_Taskproc.plan_guid = taskitem.plan_guid;
                    lN_Taskproc.plan_id = taskitem.plan_id;
                    lN_Taskproc.proj_id = Proj_id;
                    lN_Taskproc.proj_guid = NowProj_guid;

                    lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                    lN_TempTaskprocs.Add(lN_Taskproc);
                    proc++;
                }                
            }
            //插入构件
            DBService.Context.Insert(lN_TempTaskprocs);

            //更新时间等字段
            A1UpdateGJToTemp();

            //删除A1标段下的所有正式表构件
            sSQL.Clear();
            sSQL.AppendLine("delete from plN_taskproc where plan_guid='" + A1NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            //将临时表中所有数据插入
            string ssql = @"insert into plN_taskproc (proj_id,wbs_id,task_id,task_guid,seq_num,CompleteOrNot,proc_name,
                            proc_descri,est_wt,complete_pct,act_end_date,SysOrNot,target_end_date_lag,expect_end_date_lag,rsrc_id,temp_id,update_date,
                            p3ec_proc_id,p3ec_flag,proc_guid,proj_guid,plan_guid,plan_id,wbs_guid,rsrc_guid,temp_guid,est_wt_pct,keyword,formid,update_user,
                            create_date,create_user,delete_session_id,delete_date,target_end_date,proc_code,PLN_target_start_date,PLN_target_end_date,PLN_act_end_date,PLN_act_start_date)
                            select proj_id,wbs_id,task_id,task_guid,seq_num,CompleteOrNot,proc_name,proc_descri,est_wt,complete_pct,act_end_date,SysOrNot,
                            target_end_date_lag,expect_end_date_lag,rsrc_id,temp_id,update_date,p3ec_proc_id,p3ec_flag,proc_guid,proj_guid,plan_guid,plan_id,
                            wbs_guid,rsrc_guid,temp_guid,est_wt_pct,keyword,formid,update_user,create_date,create_user,delete_session_id,delete_date,target_end_date,
                            proc_code,PLN_target_start_date,PLN_target_end_date,PLN_act_end_date,PLN_act_start_date from plN_temptaskproc where plan_guid='" + A1NowPlan_guid + "' ";
            sSQL.Clear();
            sSQL.AppendLine(ssql);
            dbSQL.doSQL(sSQL.ToString());

            //删除临时表数据
            sSQL.Clear();
            sSQL.AppendLine("delete from plN_temptaskproc");
            dbSQL.doSQL(sSQL.ToString());
        }

        public static void GetA2Gj()//重新刷构件
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001002%' and proj_guid='" + NowProj_guid + "'and plan_guid='" + A2NowPlan_guid + "' " +
                "and isnull(TempTask_code,' ')=' ' ").ToList<PLN_task>();//这是正常的作业
            //List<PLN_task> Untask = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='"+ NowProj_guid + "' " +
            //    "and isnull(TempTask_code,' ')<>' ' ").ToList<PLN_task>();//这是特殊的作业（即构件转换为的作业）
            //DataTable UntaskTable = DBService.My.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
            //    "and isnull(TempTask_code,' ')<>' '").ToDataTable();

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_ebs  where  wbs_id like '001001002%' and wbs_id<> '001001002' and is_deleted<>1").ToDataTable();//A1下所有的构件
            List<PLN_taskproc> OldTask = DBService.Context.FromSql("select * from  plN_taskproc where plan_guid= '" + A2NowPlan_guid + "'  ").ToList<PLN_taskproc>();//查询原来的构件表中的数据

            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (!Gj.ContainsKey(item["ebs_id"].ToString()))
                {
                    PLN_taskproc taskitem1 = OldTask.Find(z => z.proc_code == item["ebs_id"].ToString());
                    if (taskitem1 != null)
                        Gj.Add(item["ebs_id"].ToString(), Guid.Parse(taskitem1.proc_guid));
                    else
                        Gj.Add(item["ebs_id"].ToString(), guid);

                    GjDx.Add(item["ebs_id"].ToString(), item);
                }
            }

            //foreach (DataRow item in dataTable.Rows)
            //{
            //    PLN_task taskitem = Untask.Find(z => z.TempTask_code == item["ebs_id"].ToString());
            //    if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null))
            //    {
            //        PLN_taskproc lN_Taskproc = new PLN_taskproc();

            //        lN_Taskproc.proc_id = proc;
            //        lN_Taskproc.proc_code = item["ebs_id"].ToString();
            //        lN_Taskproc.task_id = taskitem.task_id;
            //        lN_Taskproc.wbs_id = taskitem.wbs_id;
            //        lN_Taskproc.task_guid = taskitem.task_guid;
            //        lN_Taskproc.proc_name = item["ebs_name"].ToString();
            //        lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

            //        lN_Taskproc.plan_guid = taskitem.plan_guid;
            //        lN_Taskproc.plan_id = taskitem.plan_id;
            //        lN_Taskproc.proj_id = Proj_id;
            //        lN_Taskproc.proj_guid = NowProj_guid;

            //        lN_Taskproc.wbs_guid = taskitem.wbs_guid;
            //        lN_Taskprocs.Add(lN_Taskproc);
            //        proc++;
            //    }

            //}



            foreach (DataRow item in dataTable.Rows)
            {
                PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());
                if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null))
                {
                    plN_Temptaskproc lN_Taskproc = new plN_Temptaskproc();

                    lN_Taskproc.proc_id = proc;
                    lN_Taskproc.proc_code = item["ebs_id"].ToString();
                    lN_Taskproc.task_id = taskitem.task_id;
                    lN_Taskproc.wbs_id = taskitem.wbs_id;
                    lN_Taskproc.task_guid = taskitem.task_guid;
                    lN_Taskproc.proc_name = item["ebs_name"].ToString();
                    lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                    lN_Taskproc.plan_guid = taskitem.plan_guid;
                    lN_Taskproc.plan_id = taskitem.plan_id;
                    lN_Taskproc.proj_id = Proj_id;
                    lN_Taskproc.proj_guid = NowProj_guid;

                    lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                    lN_TempTaskprocs.Add(lN_Taskproc);
                    proc++;
                }
            }
            //插入构件
            DBService.Context.Insert(lN_TempTaskprocs);

            //更新时间等字段
            A2UpdateGJToTemp();

            //删除A1标段下的所有正式表构件
            sSQL.Clear();
            sSQL.AppendLine("delete from plN_taskproc where plan_guid='" + A2NowPlan_guid + "'");
            dbSQL.doSQL(sSQL.ToString());

            //将临时表中所有数据插入
            string ssql = @"insert into plN_taskproc (proj_id,wbs_id,task_id,task_guid,seq_num,CompleteOrNot,proc_name,
                            proc_descri,est_wt,complete_pct,act_end_date,SysOrNot,target_end_date_lag,expect_end_date_lag,rsrc_id,temp_id,update_date,
                            p3ec_proc_id,p3ec_flag,proc_guid,proj_guid,plan_guid,plan_id,wbs_guid,rsrc_guid,temp_guid,est_wt_pct,keyword,formid,update_user,
                            create_date,create_user,delete_session_id,delete_date,target_end_date,proc_code,PLN_target_start_date,PLN_target_end_date,PLN_act_end_date,PLN_act_start_date)
                            select proj_id,wbs_id,task_id,task_guid,seq_num,CompleteOrNot,proc_name,proc_descri,est_wt,complete_pct,act_end_date,SysOrNot,
                            target_end_date_lag,expect_end_date_lag,rsrc_id,temp_id,update_date,p3ec_proc_id,p3ec_flag,proc_guid,proj_guid,plan_guid,plan_id,
                            wbs_guid,rsrc_guid,temp_guid,est_wt_pct,keyword,formid,update_user,create_date,create_user,delete_session_id,delete_date,target_end_date,
                            proc_code,PLN_target_start_date,PLN_target_end_date,PLN_act_end_date,PLN_act_start_date from plN_temptaskproc where plan_guid='" + A2NowPlan_guid + "' ";
            sSQL.Clear();
            sSQL.AppendLine(ssql);
            dbSQL.doSQL(sSQL.ToString());

            //删除临时表数据
            sSQL.Clear();
            sSQL.AppendLine("delete from plN_temptaskproc");
            dbSQL.doSQL(sSQL.ToString());
        }

        public static void A1UpdateGJToTemp()
        {
            List<plN_Temptaskproc> Tepmtask = DBService.Context.FromSql("select * from  plN_Temptaskproc where plan_guid='" + A1NowPlan_guid + "'").ToList<plN_Temptaskproc>();//查询临时表现有的构件
            List<PLN_taskproc> OldTask = DBService.Context.FromSql("select * from  plN_taskproc where plan_guid= '" + A1NowPlan_guid + "'  ").ToList<PLN_taskproc>();//查询原来的构件表中的数据
            List<plN_Temptaskproc> newtask = new List<plN_Temptaskproc>();
            foreach (plN_Temptaskproc item in Tepmtask)
            {
                PLN_taskproc pLN_Task = OldTask.Find(z => z.proc_code == item.proc_code);
                if (pLN_Task != null)
                {
                    item.est_wt = pLN_Task.est_wt;
                    item.est_wt_pct = pLN_Task.est_wt_pct;
                    item.complete_pct = pLN_Task.complete_pct;
                    item.PLN_target_start_date = pLN_Task.PLN_target_start_date;
                    item.PLN_target_end_date = pLN_Task.PLN_target_end_date;
                    item.PLN_act_start_date = pLN_Task.PLN_act_start_date;
                    item.PLN_act_end_date = pLN_Task.PLN_act_end_date;
                    newtask.Add(item);
                }
            }
            DBService.Context.Update(newtask);//将原定工期、计划开始、结束时间刷到临时表中
        }

        public static void A2UpdateGJToTemp()
        {
            List<plN_Temptaskproc> Tepmtask = DBService.Context.FromSql("select * from  plN_Temptaskproc where plan_guid='" + A2NowPlan_guid + "'").ToList<plN_Temptaskproc>();//查询临时表现有的构件
            List<PLN_taskproc> OldTask = DBService.Context.FromSql("select * from  plN_taskproc where plan_guid= '" + A2NowPlan_guid + "'  ").ToList<PLN_taskproc>();//查询原来的构件表中的数据
            List<plN_Temptaskproc> newtask = new List<plN_Temptaskproc>();
            foreach (plN_Temptaskproc item in Tepmtask)
            {
                PLN_taskproc pLN_Task = OldTask.Find(z => z.proc_code == item.proc_code);
                if (pLN_Task != null)
                {
                    item.est_wt = pLN_Task.est_wt;
                    item.est_wt_pct = pLN_Task.est_wt_pct;
                    item.complete_pct = pLN_Task.complete_pct;
                    item.PLN_target_start_date = pLN_Task.PLN_target_start_date;
                    item.PLN_target_end_date = pLN_Task.PLN_target_end_date;
                    item.PLN_act_start_date = pLN_Task.PLN_act_start_date;
                    item.PLN_act_end_date = pLN_Task.PLN_act_end_date;
                    newtask.Add(item);
                }
            }
            DBService.Context.Update(newtask);//将原定工期、计划开始、结束时间刷到临时表中
        }



        public static void GetOrUpdateA1Gj()//获取或者更新构件的最新方法（用于处理特殊的构件作为作业的那部分数据）
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
                "and isnull(TempTask_code,' ')=' ' ").ToList<PLN_task>();//这是正常的作业
            List<PLN_task> Untask = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
                "and isnull(TempTask_code,' ')<>' ' ").ToList<PLN_task>();//这是特殊的作业（即构件转换为的作业）
            //DataTable UntaskTable = DBService.My.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
            //    "and isnull(TempTask_code,' ')<>' '").ToDataTable();


            List<PLN_taskproc> taskproc = DBService.Context.FromSql("select * from pln_taskproc where plan_guid='c3fa23df-131d-a4d8-7fbc-cdf1bb5d6d74' ").ToList<PLN_taskproc>();//这是A2现有构件

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_ebs  where  wbs_id like '001001001%' and wbs_id<> '001001001'").ToDataTable();


            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (!Gj.ContainsKey(item["ebs_id"].ToString()))
                {
                    Gj.Add(item["ebs_id"].ToString(), guid);
                    GjDx.Add(item["ebs_id"].ToString(), item);
                }
            }
            //这部分是处理特殊的构件（就是本来是构件，但是作为了PPE的作业，这部分作为作业的构件，在构件中，显示为自己本身。）
            foreach (DataRow item in dataTable.Rows)
            {
                PLN_task taskitem = Untask.Find(z => z.TempTask_code == item["ebs_id"].ToString());//是否根据视图中的ebsid能在特殊的作业中找到对应记录
                PLN_taskproc proctemp = taskproc.Find(z => z.proc_code == item["ebs_id"].ToString());//现有的A2标段中，是否已经存在了本次的ebsid，如果已经存在，则不再插入
                if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null) && proctemp == null)
                {
                    PLN_taskproc lN_Taskproc = new PLN_taskproc();

                    lN_Taskproc.proc_id = proc;
                    lN_Taskproc.proc_code = item["ebs_id"].ToString();
                    lN_Taskproc.task_id = taskitem.task_id;
                    lN_Taskproc.wbs_id = taskitem.wbs_id;
                    lN_Taskproc.task_guid = taskitem.task_guid;
                    lN_Taskproc.proc_name = item["ebs_name"].ToString();
                    lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                    lN_Taskproc.plan_guid = taskitem.plan_guid;
                    lN_Taskproc.plan_id = taskitem.plan_id;
                    lN_Taskproc.proj_id = 7;
                    lN_Taskproc.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";

                    lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                    lN_Taskprocs.Add(lN_Taskproc);
                    proc++;
                }

            }

            foreach (DataRow item in dataTable.Rows)
            {
                PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());
                PLN_taskproc proctemp = taskproc.Find(z => z.proc_code == item["ebs_id"].ToString());//现有的A2标段中，是否已经存在了本次的ebsid，如果已经存在，则不再插入
                if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null) && proctemp == null)
                {
                    PLN_taskproc lN_Taskproc = new PLN_taskproc();

                    lN_Taskproc.proc_id = proc;
                    lN_Taskproc.proc_code = item["ebs_id"].ToString();
                    lN_Taskproc.task_id = taskitem.task_id;
                    lN_Taskproc.wbs_id = taskitem.wbs_id;
                    lN_Taskproc.task_guid = taskitem.task_guid;
                    lN_Taskproc.proc_name = item["ebs_name"].ToString();
                    lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                    lN_Taskproc.plan_guid = taskitem.plan_guid;
                    lN_Taskproc.plan_id = taskitem.plan_id;
                    lN_Taskproc.proj_id = 7;
                    lN_Taskproc.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";

                    lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                    lN_Taskprocs.Add(lN_Taskproc);
                    proc++;
                }
            }
            //插入构件
            DBService.Context.Insert(lN_Taskprocs);
        }


        public static void GetOrUpdateA2Gj()//获取或者更新构件的最新方法（用于处理特殊的构件作为作业的那部分数据）
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001002%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
                "and isnull(TempTask_code,' ')=' ' ").ToList<PLN_task>();//这是正常的作业
            List<PLN_task> Untask = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001002%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
                "and isnull(TempTask_code,' ')<>' ' ").ToList<PLN_task>();//这是特殊的作业（即构件转换为的作业）
            //DataTable UntaskTable = DBService.My.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='f766507e-73ed-405e-bcac-b6e8022bf640' " +
            //    "and isnull(TempTask_code,' ')<>' '").ToDataTable();


            List<PLN_taskproc> taskproc = DBService.Context.FromSql("select * from pln_taskproc where plan_guid='8fafab0e-da05-607e-20d7-0faa24076e90' ").ToList<PLN_taskproc>();//这是A2现有构件

            DataTable dataTable = DBService.My.FromSql("select * from view_bim_ebs  where  wbs_id like '001001002%' and wbs_id<> '001001002'").ToDataTable();


            foreach (DataRow item in dataTable.Rows)
            {
                Guid guid = Guid.NewGuid();
                if (!Gj.ContainsKey(item["ebs_id"].ToString()))
                {
                    Gj.Add(item["ebs_id"].ToString(), guid);
                    GjDx.Add(item["ebs_id"].ToString(), item);
                }
            }
            //这部分是处理特殊的构件（就是本来是构件，但是作为了PPE的作业，这部分作为作业的构件，在构件中，显示为自己本身。）
            foreach (DataRow item in dataTable.Rows)
            {
                PLN_task taskitem = Untask.Find(z => z.TempTask_code == item["ebs_id"].ToString());//是否根据视图中的ebsid能在特殊的作业中找到对应记录
                PLN_taskproc proctemp = taskproc.Find(z => z.proc_code == item["ebs_id"].ToString());//现有的A2标段中，是否已经存在了本次的ebsid，如果已经存在，则不再插入
                if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null) && proctemp==null)
                {
                    PLN_taskproc lN_Taskproc = new PLN_taskproc();

                    lN_Taskproc.proc_id = proc;
                    lN_Taskproc.proc_code = item["ebs_id"].ToString();
                    lN_Taskproc.task_id = taskitem.task_id;
                    lN_Taskproc.wbs_id = taskitem.wbs_id;
                    lN_Taskproc.task_guid = taskitem.task_guid;
                    lN_Taskproc.proc_name = item["ebs_name"].ToString();
                    lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                    lN_Taskproc.plan_guid = taskitem.plan_guid;
                    lN_Taskproc.plan_id = taskitem.plan_id;
                    lN_Taskproc.proj_id = 7;
                    lN_Taskproc.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";

                    lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                    lN_Taskprocs.Add(lN_Taskproc);
                    proc++;
                }

            }

            foreach (DataRow item in dataTable.Rows)
            {
                PLN_task taskitem = task.Find(z => z.task_code == item["wbs_id"].ToString());
                PLN_taskproc proctemp = taskproc.Find(z => z.proc_code == item["ebs_id"].ToString());//现有的A2标段中，是否已经存在了本次的ebsid，如果已经存在，则不再插入
                if (taskitem != null && (lN_Taskprocs.Find(z => z.proc_code == item["ebs_id"].ToString()) == null) && proctemp == null)
                {
                    PLN_taskproc lN_Taskproc = new PLN_taskproc();

                    lN_Taskproc.proc_id = proc;
                    lN_Taskproc.proc_code = item["ebs_id"].ToString();
                    lN_Taskproc.task_id = taskitem.task_id;
                    lN_Taskproc.wbs_id = taskitem.wbs_id;
                    lN_Taskproc.task_guid = taskitem.task_guid;
                    lN_Taskproc.proc_name = item["ebs_name"].ToString();
                    lN_Taskproc.proc_guid = Gj[item["ebs_id"].ToString()].ToString();

                    lN_Taskproc.plan_guid = taskitem.plan_guid;
                    lN_Taskproc.plan_id = taskitem.plan_id;
                    lN_Taskproc.proj_id = 7;
                    lN_Taskproc.proj_guid = "f766507e-73ed-405e-bcac-b6e8022bf640";

                    lN_Taskproc.wbs_guid = taskitem.wbs_guid;
                    lN_Taskprocs.Add(lN_Taskproc);
                    proc++;
                }
            }
            //插入构件
            DBService.Context.Insert(lN_Taskprocs);
        }


        //同步时间
        public static void GetTime()
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task ").ToList<PLN_task>();
            List<PLN_taskproc> taskproc = DBService.Context.FromSql("select * from pln_taskproc ").ToList<PLN_taskproc>();

            List<PLN_taskproc> UpTaskProc = new List<PLN_taskproc>();
            //循环将值赋给构件
            foreach (PLN_task item in task)
            {
                List<PLN_taskproc> proc= taskproc.FindAll(z=>z.task_guid==item.task_guid);
                if (proc.Count>0)
                {
                    foreach (PLN_taskproc child in proc)
                    {
                        child.PLN_target_start_date = item.target_start_date;
                        child.PLN_target_end_date =item.target_end_date;
                        child.PLN_act_start_date =item.act_start_date;
                        child.PLN_act_end_date =item.act_end_date;
                        UpTaskProc.Add(child);
                    }
                }
            }

            //完成修改

            DBService.Context.Update<PLN_taskproc>(UpTaskProc);
        }

        //同步权重
        public static void GetQz()
        {
            List<PLN_task> task = DBService.Context.FromSql("select * from PLN_task ").ToList<PLN_task>();
            List<PLN_taskproc> taskproc = DBService.Context.FromSql("select * from pln_taskproc ").ToList<PLN_taskproc>();

            List<PLN_taskproc> UpTaskProc = new List<PLN_taskproc>();
            //循环将值赋给构件
            foreach (PLN_task item in task)
            {
                List<PLN_taskproc> proc = taskproc.FindAll(z => z.task_guid == item.task_guid);
                if (proc.Count > 0)
                {
                    double Bfb = float.Parse(100.ToString()) / float.Parse(proc.Count.ToString());
                    double Bfbstr = 0;
                    if (Bfb.ToString().Length>4&& Bfb<1)
                    {
                        Bfbstr=double.Parse(Bfb.ToString().Substring(0, 4));
                    }
                    else if (Bfb!=100)
                    {
                        double.Parse(Bfb.ToString().Substring(0, 5));
                    }
                    else
                    {
                        Bfbstr = Bfb;
                    }
                    for (int i = 0; i < proc.Count; i++)
                    {
                        if (i+1 == proc.Count)
                        {
                            proc[i].est_wt_pct = float.Parse((100 - (Bfbstr * i)).ToString());
                        }
                        else
                        {
                            proc[i].est_wt_pct = float.Parse(Bfbstr.ToString());
                        }
                        UpTaskProc.Add(proc[i]);
                    }
                }
            }
            DBService.Context.Update<PLN_taskproc>(UpTaskProc);
        }

        //处理WBS父子级
       
        public  static void  ChangeWBS()
        {
            List<PLN_PROJWBS> task = DBService.Context.FromSql("Select * From PLN_PROJWBS a Where plan_guid='ddcaf65e-fee0-c39c-c70e-59a76c218481'").ToList<PLN_PROJWBS>();
            DataTable dataTable = DBService.My.FromSql("select * from view_bim_wbs  where wbs_id  in(select parent_wbs_id from view_bim_wbs )  " +
              "                                       and wbs_id like '001001001%' and wbs_id<> '001001001'").ToDataTable();//A1标段是001001001，A2标段是001001002
            int num = 300;

            foreach (PLN_PROJWBS item in task)
            {
                if (item.wbs_name == "A1标段" || item.wbs_short_name.Length<3)
                {
                    continue;
                }
                PLN_PROJWBS wbs = task.Find(z => z.wbs_short_name == item.wbs_short_name.Substring(0, item.wbs_short_name.Length - 3));
                if (item.wbs_short_name.Substring(0, item.wbs_short_name.Length - 3) == "001001001")
                {
                    wbs = task.Find(z => z.wbs_short_name == "A1");
                }
                if (wbs!=null)
                {
                    item.parent_wbs_id = wbs.wbs_id;
                    item.parent_wbs_guid = wbs.wbs_guid;
                }
                else
                {
                    if (item.wbs_name=="A1标段")
                    {
                        continue;
                    }
                    //不存在，去华设里面找
                    DataRow []  dr = dataTable.Select(" wbs_id='"+item.wbs_short_name.Substring(0, item.wbs_short_name.Length-3) +"'");
                    if (dr.Length>0)
                    {
                        num++;
                        Guid id=Guid.NewGuid();
                        PLN_PROJWBS prowbs = new PLN_PROJWBS();
                        prowbs.wbs_id = num;
                        prowbs.wbs_guid = id.ToString();
                        prowbs.proj_id = 8;
                        prowbs.proj_guid = "67ceeb00-1c27-409d-9825-c9f048615b3e";//需要获取对应标段
                        prowbs.obs_id = 0;
                        prowbs.seq_num = 10;
                        prowbs.proj_node_flag = "0";
                        prowbs.sum_data_flag = "N";
                        prowbs.status_code = "0";
                        prowbs.wbs_short_name = dr[0]["wbs_id"].ToString();
                        prowbs.wbs_name = dr[0]["wbs_name"].ToString();
                        prowbs.phase_id = 0;
                        PLN_PROJWBS newwbs = task.Find(z => z.wbs_short_name == prowbs.wbs_short_name);
                        if (newwbs != null)
                        {
                            prowbs.parent_wbs_id = newwbs.wbs_id;
                            prowbs.parent_wbs_guid = newwbs.parent_wbs_guid;
                        }
                        else
                        {

                        }
                        prowbs.guid = id.ToString();
                        prowbs.update_date = DateTime.Now;
                        prowbs.plan_id = 1024;
                        prowbs.plan_guid = "ddcaf65e-fee0-c39c-c70e-59a76c218481";//需要获取对应标段
                        lN_PROJWBs.Add(prowbs);

                        item.parent_wbs_id = prowbs.wbs_id;
                        item.parent_wbs_guid = id.ToString();
                    }
                }
            }

            //DBService.Context.Update<PLN_PROJWBS>(task);
           //DBService.Context.Insert<PLN_PROJWBS>(lN_PROJWBs);
        }

        public static void ChangeTask()
        {
            List<PLN_task> task = DBService.Context.FromSql("Select * From PLN_task a Where plan_guid='ddcaf65e-fee0-c39c-c70e-59a76c218481'").ToList<PLN_task>();
            List<PLN_PROJWBS> wbs = DBService.Context.FromSql("Select * From PLN_PROJWBS a Where plan_guid='ddcaf65e-fee0-c39c-c70e-59a76c218481'").ToList<PLN_PROJWBS>();
            List<PLN_task> updatetask = new List<PLN_task>();
            foreach (PLN_task item in task)
            {
                PLN_PROJWBS wbsinfo = wbs.Find(z => z.wbs_short_name == item.task_code.Substring(0, item.task_code.Length - 3));
                if (wbsinfo != null&&(item.wbs_id!= wbsinfo.wbs_id||item.wbs_guid!= wbsinfo.wbs_guid))
                {
                    item.wbs_guid = wbsinfo.wbs_guid;
                    item.wbs_id = wbsinfo.wbs_id;
        
                    updatetask.Add(item);
                }
            }
            DBService.Context.Delete<PLN_task>(updatetask);
        }


        public  static void DelWBS()
        {
            List<PLN_PROJWBS> task = DBService.Context.FromSql("Select * From PLN_PROJWBS a Where plan_guid='c127217f-1ffa-ab2e-b61f-2ce035f54dd9'").ToList<PLN_PROJWBS>();

            ArrayList arrayList=new ArrayList();
            List<PLN_PROJWBS> deltask = new List<PLN_PROJWBS>();
            foreach (PLN_PROJWBS item in task)
            {
                PLN_PROJWBS wbs = task.Find(z => z.wbs_guid == item.parent_wbs_guid);
                if (wbs==null)
                {
                    deltask.Add(item);
                }
            }

            DBService.Context.Delete<PLN_PROJWBS>(deltask);
        }


        public static void DelA2()
        {
            List<PLN_PROJWBS> task = DBService.Context.FromSql("Select * From PLN_PROJWBS a Where plan_guid='ddcaf65e-fee0-c39c-c70e-59a76c218481'").ToList<PLN_PROJWBS>();
            ArrayList arrayList = new ArrayList();
            List<PLN_PROJWBS> deltask = new List<PLN_PROJWBS>();
            foreach (PLN_PROJWBS item in task)
            {
                if (arrayList.Contains(item.wbs_short_name))
                {
                    item.wbs_id = -1;
                    deltask.Add(item);
                }
                else
                {
                    arrayList.Add(item.wbs_short_name);
                }    
               
            }

            DBService.Context.Update<PLN_PROJWBS>(deltask);
        }


        public  static void DelCF()
        {
            List<PLN_PROJWBS> task = DBService.Context.FromSql("Select * From PLN_PROJWBS a Where plan_guid='ddcaf65e-fee0-c39c-c70e-59a76c218481'").ToList<PLN_PROJWBS>();
            List<PLN_task> taskinfo = DBService.Context.FromSql("select * from PLN_task where task_Code LIKE '001001001%' and proj_guid='67ceeb00-1c27-409d-9825-c9f048615b3e'").ToList<PLN_task>();

            ArrayList arrayList = new ArrayList();
            List<PLN_PROJWBS> deltask = new List<PLN_PROJWBS>();

            foreach (PLN_PROJWBS item in task)
            {
                PLN_PROJWBS pLN_ = task.Find(z=>z.parent_wbs_guid==item.wbs_guid);
                PLN_task pLN_Task = taskinfo.Find(z => z.wbs_guid == item.wbs_guid);
                List<PLN_PROJWBS> aaa = task.FindAll(z => z.wbs_short_name == item.wbs_short_name);
                if (pLN_==null&& pLN_Task==null&& aaa.Count>1)
                {
                    item.wbs_id = -1;
                    deltask.Add(item);
                }    
            }
            DBService.Context.Update<PLN_PROJWBS>(deltask);
        }

        public static void UpdateGJ()//刷新构件序号
        {
            List<PLN_taskproc> PLN_taskprocList = DBService.Context.FromSql("select * from PLN_TaskProc where plan_guid='"+ A1NowPlan_guid + "' or plan_guid='"+ A2NowPlan_guid + "'  order by proc_code").ToList<PLN_taskproc>();
            int XH = 1;
            foreach (PLN_taskproc temp in PLN_taskprocList)
            {
                temp.seq_num = XH;
                XH++;
            }
            DBService.Context.Update(PLN_taskprocList);
        }


        public static void UpdateGX()//刷新工序序号
        {
            List<PS_PLN_TaskProc_Sub> TaskProc_SubList = DBService.Context.FromSql("select * from PS_PLN_TaskProc_Sub where plan_guid='"+ A1NowPlan_guid + "' " +
                "or plan_guid='"+ A2NowPlan_guid + "' order by procsub_code").ToList<PS_PLN_TaskProc_Sub>();
            int XH = 1;
            foreach (PS_PLN_TaskProc_Sub temp in TaskProc_SubList)
            {
                temp.seq_num = XH;
                XH++;
            }
            DBService.Context.Update(TaskProc_SubList);
        }

        public static void UpdateWBSXH()//更新wbs序号
        {
            List<PLN_PROJWBS> TaskProc_SubList = DBService.Context.FromSql("Select * From  PLN_PROJWBS A Where   (0=0)  and " +
                "(A.plan_guid='"+ A2NowPlan_guid + "' or A.plan_guid='"+ A1NowPlan_guid + "') and 1=1  Order By  A.wbs_short_name asc ").ToList<PLN_PROJWBS>();
            int XH = 1;
            foreach (PLN_PROJWBS temp in TaskProc_SubList)
            {
                temp.seq_num = XH;
                XH++;
            }
            DBService.Context.Update(TaskProc_SubList);
        }


        static List<PS_PLN_TaskProc_Sub> TaskProc_Sub = DBService.Context.FromSql("select * from PS_PLN_TaskProc_Sub").ToList<PS_PLN_TaskProc_Sub>();

        static List<PLN_taskproc> taskproc = DBService.Context.FromSql("select  * from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid)").ToList<PLN_taskproc>();

        public static void UpdateGXWet()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            StringBuilder sPS_MDM = new StringBuilder();
            StringBuilder sBOQ = new StringBuilder();
            StringBuilder UpadateSQL = new StringBuilder();

            //List<PS_PLN_TaskProc_Sub> TaskProc_Sub = DBService.Context.FromSql("select  * from PS_PLN_TaskProc_Sub").ToList<PS_PLN_TaskProc_Sub>();

            //List<PLN_taskproc> taskproc = DBService.Context.FromSql("select  * from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid)").ToList<PLN_taskproc>();
            List<PS_MDM_MeasureModelDefine_Dtl> MeasureModelDefine = DBService.Context.FromSql("select * from PS_MDM_MeasureModelDefine_Dtl ").ToList<PS_MDM_MeasureModelDefine_Dtl>();


            sPS_MDM.Clear();
            sPS_MDM.AppendLine("select top 30 ID from PS_MDM_MeasureModelDefine order by sequ desc ");
            DataSet dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
            for (int i = 0; i < dsSQL.Tables[0].Rows.Count; i++)//循环主表，根据主表的id关联子表的masterid，查询出子表的结构
            {
                List<string> stepList = new List<string>();
                string masterid = dsSQL.Tables[0].Rows[i]["ID"].ToString();

                List<PS_MDM_MeasureModelDefine_Dtl> MeasureModelDefineList = MeasureModelDefine.FindAll(z => z.MasterId == Guid.Parse(masterid));
                for (int j = 0; j < MeasureModelDefineList.Count; j++)
                {
                    stepList.Add(MeasureModelDefineList[j].Step.ToString());//子表的工序模板
                }

                List<PLN_taskproc> taskprocTemp = new List<PLN_taskproc>();

                //查询每个存在工序的构件
                for (int k = 0; k < taskproc.Count; k++)
                {
                    List<string> PS_PLN_TaskProc_SubList = new List<string>();

                    List<PS_PLN_TaskProc_Sub> ListTaskProc_Sub = TaskProc_Sub.FindAll(z => z.proc_guid == taskproc[k].proc_guid);
                    for (int e = 0; e < ListTaskProc_Sub.Count; e++)//各个构件对应的工序列表
                    {
                        PS_PLN_TaskProc_SubList.Add(ListTaskProc_Sub[e].ProcSub_Name.ToString());
                    }
                    if (stepList.All(PS_PLN_TaskProc_SubList.Contains) && stepList.Count == PS_PLN_TaskProc_SubList.Count)//如果存在相同的，则更新权重、权重百分比
                    {
                        foreach (PS_MDM_MeasureModelDefine_Dtl item in MeasureModelDefineList)
                        {
                            PS_PLN_TaskProc_Sub pS_PLN_Task = ListTaskProc_Sub.Find(z => z.ProcSub_Name == item.Step);

                            PS_PLN_TaskProc_Sub UpSub = TaskProc_Sub.Find(x => x.ProcSub_guid == pS_PLN_Task.ProcSub_guid);
                            UpSub.est_wt =int.Parse(item.Weight.ToString());
                            UpSub.est_wt_pct = item.TotalWeight;


                            //UpadateSQL.Clear();
                            //UpadateSQL.AppendLine("update PS_PLN_TaskProc_Sub set est_wt=@est_wt,est_wt_pct=@est_wt_pct ");
                            //UpadateSQL.AppendLine(" where ProcSub_guid=@ProcSub_guid ");
                            //dbSQL.addParam("est_wt", item.Weight);
                            //dbSQL.addParam("est_wt_pct", item.TotalWeight);
                            //dbSQL.addParam("ProcSub_guid", pS_PLN_Task.ProcSub_guid);
                            //dbSQL.doSQL(UpadateSQL.ToString());

                        }
                        taskprocTemp.Add(taskproc[k]);
                    }
                }
                for (int k = 0; k < taskprocTemp.Count; k++)
                {
                    taskproc.Remove(taskprocTemp[k]);
                }               

            }
            List<PS_PLN_TaskProc_Sub> TaskProc_Sub1 = TaskProc_Sub;
            //DBService.Context.Update(TaskProc_Sub);
        }


        public static void UpdateGXWet2()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            StringBuilder sPS_MDM = new StringBuilder();
            StringBuilder sBOQ = new StringBuilder();
            StringBuilder UpadateSQL = new StringBuilder();

            //List<PS_PLN_TaskProc_Sub> TaskProc_Sub = DBService.Context.FromSql("select * from PS_PLN_TaskProc_Sub").ToList<PS_PLN_TaskProc_Sub>();

            //List<PLN_taskproc> taskproc = DBService.Context.FromSql("select  * from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid)").ToList<PLN_taskproc>();
            List<PS_MDM_MeasureModelDefine_Dtl> MeasureModelDefine = DBService.Context.FromSql("select * from PS_MDM_MeasureModelDefine_Dtl ").ToList<PS_MDM_MeasureModelDefine_Dtl>();


            sPS_MDM.Clear();
            sPS_MDM.AppendLine("select top 34 ID from PS_MDM_MeasureModelDefine order by sequ  ");
            DataSet dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
            for (int i = 0; i < dsSQL.Tables[0].Rows.Count; i++)//循环主表，根据主表的id关联子表的masterid，查询出子表的结构
            {
                List<string> stepList = new List<string>();
                string masterid = dsSQL.Tables[0].Rows[i]["ID"].ToString();

                List<PS_MDM_MeasureModelDefine_Dtl> MeasureModelDefineList = MeasureModelDefine.FindAll(z => z.MasterId == Guid.Parse(masterid));
                for (int j = 0; j < MeasureModelDefineList.Count; j++)
                {
                    stepList.Add(MeasureModelDefineList[j].Step.ToString());//子表的工序模板
                }

                List<PLN_taskproc> taskprocTemp = new List<PLN_taskproc>();

                //查询每个存在工序的构件
                for (int k = 0; k < taskproc.Count; k++)
                {
                    List<string> PS_PLN_TaskProc_SubList = new List<string>();

                    List<PS_PLN_TaskProc_Sub> ListTaskProc_Sub = TaskProc_Sub.FindAll(z => z.proc_guid == taskproc[k].proc_guid);
                    for (int e = 0; e < ListTaskProc_Sub.Count; e++)//各个构件对应的工序列表
                    {
                        PS_PLN_TaskProc_SubList.Add(ListTaskProc_Sub[e].ProcSub_Name.ToString());
                    }
                    if (stepList.All(PS_PLN_TaskProc_SubList.Contains) && stepList.Count == PS_PLN_TaskProc_SubList.Count)//如果存在相同的，则更新权重、权重百分比
                    {
                        foreach (PS_MDM_MeasureModelDefine_Dtl item in MeasureModelDefineList)
                        {
                            PS_PLN_TaskProc_Sub pS_PLN_Task = ListTaskProc_Sub.Find(z => z.ProcSub_Name == item.Step);

                            PS_PLN_TaskProc_Sub UpSub = TaskProc_Sub.Find(x => x.ProcSub_guid == pS_PLN_Task.ProcSub_guid);
                            UpSub.est_wt = int.Parse(item.Weight.ToString());
                            UpSub.est_wt_pct = item.TotalWeight;


                            //UpadateSQL.Clear();
                            //UpadateSQL.AppendLine("update PS_PLN_TaskProc_Sub set est_wt=@est_wt,est_wt_pct=@est_wt_pct ");
                            //UpadateSQL.AppendLine(" where ProcSub_guid=@ProcSub_guid ");
                            //dbSQL.addParam("est_wt", item.Weight);
                            //dbSQL.addParam("est_wt_pct", item.TotalWeight);
                            //dbSQL.addParam("ProcSub_guid", pS_PLN_Task.ProcSub_guid);
                            //dbSQL.doSQL(UpadateSQL.ToString());

                        }
                        taskprocTemp.Add(taskproc[k]);
                    }
                }
                for (int k = 0; k < taskprocTemp.Count; k++)
                {
                    taskproc.Remove(taskprocTemp[k]);
                }

            }
            List<PS_PLN_TaskProc_Sub> TaskProc_Sub1 = TaskProc_Sub;
            //DBService.Context.Update(TaskProc_Sub);
        }

        public static int GetMaxXH()
        {
            int XH = 0;
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select isnull(MAX(sequ),0) as sequ from PS_MDM_MeasureModelDefine  ");//存在工序的构件
            DataSet PS_MDM_MeasureModelDefine = dbSQL.getDataSet(sSQL.ToString());
            if (PS_MDM_MeasureModelDefine.Tables[0].Rows.Count > 0)
            {
                XH = int.Parse(PS_MDM_MeasureModelDefine.Tables[0].Rows[0]["sequ"].ToString())+1;
            }
            else
                XH = 0;
            return XH;
        }


        public static void SelParent(string parent_wbs_guid,ref string Title)
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            sSQL.AppendLine("select  wbs_name,parent_wbs_guid  from pln_projwbs where wbs_guid=@wbs_guid and proj_id=10");
            dbSQL.addParam("wbs_guid", parent_wbs_guid);
            DataSet dsSQL = dbSQL.getDataSet(sSQL.ToString());
            if (dsSQL.Tables[0].Rows.Count > 0)
            {
                Title = dsSQL.Tables[0].Rows[0]["wbs_name"].ToString()+","+ Title;
                SelParent(dsSQL.Tables[0].Rows[0]["parent_wbs_guid"].ToString(),ref Title);
            }
        }

        public static void InsertGXMBSDTS()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            StringBuilder sPS_MDM = new StringBuilder();
            StringBuilder sBOQ = new StringBuilder();
            StringBuilder InsertSQL = new StringBuilder();
            List<PS_PLN_TaskProc_Sub> TaskProc_SubList = DBService.Context.FromSql("select * from PS_PLN_TaskProc_Sub  where (plan_guid='"+ A1NowPlan_guid + "' or plan_guid='"+ A2NowPlan_guid + "')").ToList<PS_PLN_TaskProc_Sub>();
            List<PS_MDM_MeasureModelDefine> MeasureModelDefine = DBService.Context.FromSql("select * from PS_MDM_MeasureModelDefine ").ToList<PS_MDM_MeasureModelDefine>();
            List<PS_MDM_MeasureModelDefine_Dtl> MeasureModelDefine_Dtl = DBService.Context.FromSql("select * from PS_MDM_MeasureModelDefine_Dtl ").ToList<PS_MDM_MeasureModelDefine_Dtl>();
            sSQL.Clear();
            sSQL.AppendLine("select proc_name,proc_guid from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid)and (plan_guid='"+ A1NowPlan_guid + "' or plan_guid='"+ A2NowPlan_guid + "') ");//存在工序的构件
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());

            for (int k = 0; k < PLN_taskproc.Tables[0].Rows.Count; k++)
            {
                string proc_name = PLN_taskproc.Tables[0].Rows[k]["proc_name"].ToString();
                List<PS_PLN_TaskProc_Sub> TempSubList = new List<PS_PLN_TaskProc_Sub>();//临时记录工序数据
                List<string> PS_PLN_TaskProc_SubList = new List<string>();

                List<PS_PLN_TaskProc_Sub>  Temp = TaskProc_SubList.FindAll(Z=> Z.proc_guid.ToUpper() == PLN_taskproc.Tables[0].Rows[k]["proc_guid"].ToString().ToUpper());//在工序中找到所有proc_guid的数据组成集合
                for (int e = 0; e < Temp.Count; e++)//各个构件对应的工序列表
                {
                    PS_PLN_TaskProc_SubList.Add(Temp[e].ProcSub_Name.ToString());//将工序名称插入到数据中
                }
                for (int p = 0; p < Temp.Count; p++)
                {
                    PS_PLN_TaskProc_Sub TempSub = new PS_PLN_TaskProc_Sub();
                    TempSub.seq_num =int.Parse(Temp[p].seq_num.ToString());
                    TempSub.ProcSub_Name = Temp[p].ProcSub_Name.ToString();
                    TempSubList.Add(TempSub);
                }
             
                if (MeasureModelDefine.Count > 0)
                {
                    Boolean SFCR = false;
                    for (int i = 0; i < MeasureModelDefine.Count; i++)//循环主表，根据主表的id关联子表的masterid，查询出子表的结构
                    {
                        List<string> stepList = new List<string>();
                        string masterid = MeasureModelDefine[i].Id.ToString();
                        List<PS_MDM_MeasureModelDefine_Dtl> TempMeasureModelDefine_Dtl = MeasureModelDefine_Dtl.FindAll(Z => Z.MasterId ==Guid.Parse(masterid));

                        for (int j = 0; j < TempMeasureModelDefine_Dtl.Count; j++)
                        {
                            stepList.Add(TempMeasureModelDefine_Dtl[j].Step.ToString());//子表的工序模板
                        }
                        if (stepList.All(PS_PLN_TaskProc_SubList.Contains) && stepList.Count == PS_PLN_TaskProc_SubList.Count)//如果结构相同，则返回true
                        {
                            SFCR = true;
                            break;
                        }
                    }
                    if (!SFCR)
                    {
                        string Title = "";
                        string task_guid = "";
                        //查出对应的第一个构件
                        sPS_MDM.Clear();
                        sPS_MDM.AppendLine("select top 1 proc_name,task_guid from PLN_TaskProc where task_guid=(select task_guid from PLN_TaskProc where proc_guid=@proc_guid) order by seq_num ");
                        dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[k]["proc_guid"].ToString());
                        DataSet dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                        if (dsSQL.Tables[0].Rows.Count > 0)
                        {
                            Title = dsSQL.Tables[0].Rows[0]["proc_name"].ToString();
                            task_guid = dsSQL.Tables[0].Rows[0]["task_guid"].ToString();
                        }
                        string task_code = "";
                        //查出对应的作业
                        sPS_MDM.Clear();
                        sPS_MDM.AppendLine("select task_code,task_name from PLN_task where task_guid=@task_guid ");
                        dbSQL.addParam("task_guid", task_guid);
                        dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                        if (dsSQL.Tables[0].Rows.Count > 0)
                        {
                            Title = dsSQL.Tables[0].Rows[0]["task_name"].ToString() + "," + Title;
                            task_code = dsSQL.Tables[0].Rows[0]["task_code"].ToString();
                        }

                        string parent_wbs_guid = "";
                        string sXH = "";
                        sPS_MDM.Clear();
                        sPS_MDM.AppendLine("select seq_num,wbs_name,parent_wbs_guid from pln_projwbs where wbs_short_name=@wbs_short_name and proj_id='"+ Proj_id + "' ");
                        dbSQL.addParam("wbs_short_name", task_code.Substring(0, task_code.Length - 3));
                        dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                        if (dsSQL.Tables[0].Rows.Count > 0)
                        {
                            Title = dsSQL.Tables[0].Rows[0]["wbs_name"].ToString() + "," + Title;
                            parent_wbs_guid = dsSQL.Tables[0].Rows[0]["parent_wbs_guid"].ToString();
                            sXH = dsSQL.Tables[0].Rows[0]["seq_num"].ToString();
                        }
                        SelParent(parent_wbs_guid, ref Title);

                        PS_MDM_MeasureModelDefine InsertDefine = new PS_MDM_MeasureModelDefine();
                        int XH = GetMaxXH();
                        Guid id = Guid.NewGuid();
                        InsertSQL.Clear();
                        InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine( ");
                        InsertSQL.AppendLine("Id,Code,Title,BizAreaId,Sequ,Status,RegHumId,RegHumName,RegDate, ");
                        InsertSQL.AppendLine("RegPosiId,RegDeptId,EpsProjId,RecycleHumId,UpdHumId,UpdHumName,UpdDate,ApprHumId,OwnProjId,OwnProjName ) ");
                        InsertSQL.AppendLine("values ( ");
                        InsertSQL.AppendLine("@Id,@Code,@Title,@BizAreaId,@Sequ,@Status,@RegHumId,@RegHumName,@RegDate, ");
                        InsertSQL.AppendLine("@RegPosiId,@RegDeptId,@EpsProjId,@RecycleHumId,@UpdHumId,@UpdHumName,@UpdDate,@ApprHumId,@OwnProjId,@OwnProjName ) ");
                        dbSQL.addParam("Id", id);
                        dbSQL.addParam("Code", XH);
                        dbSQL.addParam("Title", Title);
                        dbSQL.addParam("BizAreaId", "00000000-0000-0000-0000-00000000000A");
                        dbSQL.addParam("Sequ", sXH);
                        dbSQL.addParam("Status", "0");
                        dbSQL.addParam("RegHumId", "AD000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("RegHumName", "系统管理员");
                        dbSQL.addParam("RegDate", DateTime.Now);
                        dbSQL.addParam("RegPosiId", "00000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("RegDeptId", "00000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("EpsProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                        dbSQL.addParam("RecycleHumId", "AD000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("UpdHumId", "AD000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("UpdHumName", "系统管理员");
                        dbSQL.addParam("UpdDate", DateTime.Now);
                        dbSQL.addParam("ApprHumId", "00000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("OwnProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                        dbSQL.addParam("OwnProjName", "江阴靖江长江隧道工程");
                        dbSQL.doSQL(InsertSQL.ToString());
                        //如果插入成功，则向数组中插入一条，这里是为了不再查询PS_MDM_MeasureModelDefine表，减少访问数据库，提升速度
                        InsertDefine.Id = id;
                        MeasureModelDefine.Add(InsertDefine);

                        for (int u = 0; u < TempSubList.Count; u++)
                        {
                            PS_MDM_MeasureModelDefine_Dtl InsertDefine_Dtl = new PS_MDM_MeasureModelDefine_Dtl();
                            Guid ZBid = Guid.NewGuid();
                            InsertSQL.Clear();
                            InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine_Dtl (");
                            InsertSQL.AppendLine("Id,MasterId,Sequ,Step,Weight,TotalWeight,Quantities,isMain,UpdDate,rsrc_guid )");
                            InsertSQL.AppendLine("values (");
                            InsertSQL.AppendLine("@Id,@MasterId,@Sequ,@Step,@Weight,@TotalWeight,@Quantities,@isMain,@UpdDate,@rsrc_guid )");
                            dbSQL.addParam("Id", ZBid);
                            dbSQL.addParam("MasterId", id);
                            dbSQL.addParam("Sequ", TempSubList[u].seq_num.ToString());
                            dbSQL.addParam("Step", TempSubList[u].ProcSub_Name.ToString());
                            dbSQL.addParam("Weight", "0");
                            dbSQL.addParam("TotalWeight", "0");
                            dbSQL.addParam("Quantities", "0");
                            dbSQL.addParam("isMain", "0");
                            dbSQL.addParam("UpdDate", DateTime.Now);
                            dbSQL.addParam("rsrc_guid", "00000000-0000-0000-0000-000000000000");
                            dbSQL.doSQL(InsertSQL.ToString());
                            //如果插入成功，则向数组中插入一条，这里是为了不再查询PS_MDM_MeasureModelDefine_Dtl表，减少访问数据库，提升速度
                            InsertDefine_Dtl.Id = ZBid;
                            InsertDefine_Dtl.Step = PS_PLN_TaskProc_SubList[u].ToString();
                            InsertDefine_Dtl.MasterId = id;
                            MeasureModelDefine_Dtl.Add(InsertDefine_Dtl);

                        }

                    }
                }
                else
                {
                     string Title = "";
                    string task_guid = "";
                    //查出对应的第一个构件
                    sPS_MDM.Clear();
                    sPS_MDM.AppendLine("select top 1 proc_name,task_guid from PLN_TaskProc where task_guid=(select task_guid from PLN_TaskProc where proc_guid=@proc_guid) order by seq_num ");
                    dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[k]["proc_guid"].ToString());
                    DataSet dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                    if (dsSQL.Tables[0].Rows.Count > 0)
                    {
                        Title = dsSQL.Tables[0].Rows[0]["proc_name"].ToString();
                        task_guid = dsSQL.Tables[0].Rows[0]["task_guid"].ToString();
                    }
                    string task_code = "";
                    //查出对应的作业
                    sPS_MDM.Clear();
                    sPS_MDM.AppendLine("select task_code,task_name from PLN_task where task_guid=@task_guid ");
                    dbSQL.addParam("task_guid", task_guid);
                    dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                    if (dsSQL.Tables[0].Rows.Count > 0)
                    {
                        Title = dsSQL.Tables[0].Rows[0]["task_name"].ToString()+","+ Title;
                        task_code = dsSQL.Tables[0].Rows[0]["task_code"].ToString();
                    }

                    string sXH = "";
                    string parent_wbs_guid = "";
                    sPS_MDM.Clear();
                    sPS_MDM.AppendLine("select seq_num,wbs_name,parent_wbs_guid from pln_projwbs where wbs_short_name=@wbs_short_name and proj_id='"+ Proj_id + "' ");
                    dbSQL.addParam("wbs_short_name", task_code.Substring(0, task_code.Length-3));
                    dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                    if (dsSQL.Tables[0].Rows.Count > 0)
                    {
                        Title = dsSQL.Tables[0].Rows[0]["wbs_name"].ToString() + "," + Title;
                        parent_wbs_guid = dsSQL.Tables[0].Rows[0]["parent_wbs_guid"].ToString();
                        sXH= dsSQL.Tables[0].Rows[0]["seq_num"].ToString();
                    }
                    SelParent(parent_wbs_guid,ref Title);

                    PS_MDM_MeasureModelDefine InsertDefine = new PS_MDM_MeasureModelDefine();
                    int XH = GetMaxXH();
                    Guid id = Guid.NewGuid();
                    InsertSQL.Clear();
                    InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine( ");
                    InsertSQL.AppendLine("Id,Code,Title,BizAreaId,Sequ,Status,RegHumId,RegHumName,RegDate, ");
                    InsertSQL.AppendLine("RegPosiId,RegDeptId,EpsProjId,RecycleHumId,UpdHumId,UpdHumName,UpdDate,ApprHumId,OwnProjId,OwnProjName ) ");
                    InsertSQL.AppendLine("values ( ");
                    InsertSQL.AppendLine("@Id,@Code,@Title,@BizAreaId,@Sequ,@Status,@RegHumId,@RegHumName,@RegDate, ");
                    InsertSQL.AppendLine("@RegPosiId,@RegDeptId,@EpsProjId,@RecycleHumId,@UpdHumId,@UpdHumName,@UpdDate,@ApprHumId,@OwnProjId,@OwnProjName ) ");
                    dbSQL.addParam("Id", id);
                    dbSQL.addParam("Code", XH);
                    dbSQL.addParam("Title", Title);
                    dbSQL.addParam("BizAreaId", "00000000-0000-0000-0000-00000000000A");
                    dbSQL.addParam("Sequ", sXH);
                    dbSQL.addParam("Status", "0");
                    dbSQL.addParam("RegHumId", "AD000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("RegHumName", "系统管理员");
                    dbSQL.addParam("RegDate", DateTime.Now);
                    dbSQL.addParam("RegPosiId", "00000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("RegDeptId", "00000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("EpsProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                    dbSQL.addParam("RecycleHumId", "AD000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("UpdHumId", "AD000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("UpdHumName", "系统管理员");
                    dbSQL.addParam("UpdDate", DateTime.Now);
                    dbSQL.addParam("ApprHumId", "00000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("OwnProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                    dbSQL.addParam("OwnProjName", "江阴靖江长江隧道工程");
                    dbSQL.doSQL(InsertSQL.ToString());
                    //如果插入成功，则向数组中插入一条，这里是为了不再查询PS_MDM_MeasureModelDefine表，减少访问数据库，提升速度
                    InsertDefine.Id = id;
                    MeasureModelDefine.Add(InsertDefine);

                    sSQL.Clear();
                    sSQL.AppendLine("select proc_name,proc_guid from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid) ");//存在工序的构件
                    PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
                    if (PLN_taskproc.Tables[0].Rows.Count > 0)
                    {
                        sBOQ.Clear();
                        sBOQ.AppendLine("select seq_num,ProcSub_Name from PS_PLN_TaskProc_Sub where proc_guid=@proc_guid ");
                        dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[0]["proc_guid"].ToString());
                        DataSet PS_PLN_TaskProc_Sub = dbSQL.getDataSet(sBOQ.ToString());
                        for (int u = 0; u < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; u++)
                        {
                            PS_MDM_MeasureModelDefine_Dtl InsertDefine_Dtl = new PS_MDM_MeasureModelDefine_Dtl();
                            Guid ZBid = Guid.NewGuid();
                            InsertSQL.Clear();
                            InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine_Dtl (");
                            InsertSQL.AppendLine("Id,MasterId,Sequ,Step,Weight,TotalWeight,Quantities,isMain,UpdDate,rsrc_guid )");
                            InsertSQL.AppendLine("values (");
                            InsertSQL.AppendLine("@Id,@MasterId,@Sequ,@Step,@Weight,@TotalWeight,@Quantities,@isMain,@UpdDate,@rsrc_guid )");
                            dbSQL.addParam("Id", ZBid);
                            dbSQL.addParam("MasterId", id);
                            dbSQL.addParam("Sequ", PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["seq_num"].ToString());
                            dbSQL.addParam("Step", PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["ProcSub_Name"].ToString());
                            dbSQL.addParam("Weight", "0");
                            dbSQL.addParam("TotalWeight", "0");
                            dbSQL.addParam("Quantities", "0");
                            dbSQL.addParam("isMain", "0");
                            dbSQL.addParam("UpdDate", DateTime.Now);
                            dbSQL.addParam("rsrc_guid", "00000000-0000-0000-0000-000000000000");
                            dbSQL.doSQL(InsertSQL.ToString());
                            //如果插入成功，则向数组中插入一条，这里是为了不再查询PS_MDM_MeasureModelDefine_Dtl表，减少访问数据库，提升速度
                            InsertDefine_Dtl.Id = ZBid;
                            InsertDefine_Dtl.Step = PS_PLN_TaskProc_SubList[u].ToString();
                            InsertDefine_Dtl.MasterId = id;
                            MeasureModelDefine_Dtl.Add(InsertDefine_Dtl);
                        }
                    }
                }

            }
        }


        public static void InsertGXMB2()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            StringBuilder sPS_MDM = new StringBuilder();
            StringBuilder sBOQ = new StringBuilder();
            StringBuilder InsertSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select proc_name,proc_guid from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid) ");//存在工序的构件
            DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
            
            for (int k = 0; k < PLN_taskproc.Tables[0].Rows.Count; k++)
            {
                string proc_name = PLN_taskproc.Tables[0].Rows[k]["proc_name"].ToString();
                List<string> PS_PLN_TaskProc_SubList = new List<string>();
                sBOQ.Clear();
                sBOQ.AppendLine("select ProcSub_Name from PS_PLN_TaskProc_Sub where proc_guid=@proc_guid ");
                dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[k]["proc_guid"].ToString());
                DataSet PS_PLN_TaskProc_Sub = dbSQL.getDataSet(sBOQ.ToString());
                for (int e = 0; e < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; e++)//各个构件对应的工序列表
                {
                    PS_PLN_TaskProc_SubList.Add(PS_PLN_TaskProc_Sub.Tables[0].Rows[e]["ProcSub_Name"].ToString());
                }

                sPS_MDM.Clear();
                sPS_MDM.AppendLine("select ID from PS_MDM_MeasureModelDefine ");
                DataSet dsSQL = dbSQL.getDataSet(sPS_MDM.ToString());
                if (dsSQL.Tables[0].Rows.Count > 0)
                {
                    Boolean SFCR = false;
                    for (int i = 0; i < dsSQL.Tables[0].Rows.Count; i++)//循环主表，根据主表的id关联子表的masterid，查询出子表的结构
                    {
                        List<string> stepList = new List<string>();
                        string masterid = dsSQL.Tables[0].Rows[i]["ID"].ToString();
                        sPS_MDM.Clear();
                        sPS_MDM.AppendLine("select step from PS_MDM_MeasureModelDefine_Dtl where masterid=@masterid ");
                        dbSQL.addParam("masterid", masterid);
                        DataSet dsSQL1 = dbSQL.getDataSet(sPS_MDM.ToString());
                        for (int j = 0; j < dsSQL1.Tables[0].Rows.Count; j++)
                        {
                            stepList.Add(dsSQL1.Tables[0].Rows[j]["step"].ToString());//子表的工序模板
                        }
                        if (stepList.All(PS_PLN_TaskProc_SubList.Contains) && stepList.Count == PS_PLN_TaskProc_SubList.Count)
                        {
                            SFCR = true;
                            break;
                        }                   
                    }
                    if (!SFCR)
                    {
                        Guid id = Guid.NewGuid();
                        InsertSQL.Clear();
                        InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine( ");
                        InsertSQL.AppendLine("Id,Code,Title,BizAreaId,Sequ,Status,RegHumId,RegHumName,RegDate, ");
                        InsertSQL.AppendLine("RegPosiId,RegDeptId,EpsProjId,RecycleHumId,UpdHumId,UpdHumName,UpdDate,ApprHumId,OwnProjId,OwnProjName ) ");
                        InsertSQL.AppendLine("values ( ");
                        InsertSQL.AppendLine("@Id,@Code,@Title,@BizAreaId,@Sequ,@Status,@RegHumId,@RegHumName,@RegDate, ");
                        InsertSQL.AppendLine("@RegPosiId,@RegDeptId,@EpsProjId,@RecycleHumId,@UpdHumId,@UpdHumName,@UpdDate,@ApprHumId,@OwnProjId,@OwnProjName ) ");
                        dbSQL.addParam("Id", id);
                        dbSQL.addParam("Code", GetMaxXH());
                        dbSQL.addParam("Title", proc_name);
                        dbSQL.addParam("BizAreaId", "00000000-0000-0000-0000-00000000000A");
                        dbSQL.addParam("Sequ", GetMaxXH());
                        dbSQL.addParam("Status", "0");
                        dbSQL.addParam("RegHumId", "AD000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("RegHumName", "系统管理员");
                        dbSQL.addParam("RegDate", DateTime.Now);
                        dbSQL.addParam("RegPosiId", "00000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("RegDeptId", "00000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("EpsProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                        dbSQL.addParam("RecycleHumId", "AD000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("UpdHumId", "AD000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("UpdHumName", "系统管理员");
                        dbSQL.addParam("UpdDate", DateTime.Now);
                        dbSQL.addParam("ApprHumId", "00000000-0000-0000-0000-000000000000");
                        dbSQL.addParam("OwnProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                        dbSQL.addParam("OwnProjName", "江阴靖江长江隧道工程");
                        dbSQL.doSQL(InsertSQL.ToString());

                        for (int u = 0; u < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; u++)
                        {
                            Guid ZBid = Guid.NewGuid();
                            InsertSQL.Clear();
                            InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine_Dtl (");
                            InsertSQL.AppendLine("Id,MasterId,Sequ,Step,Weight,TotalWeight,Quantities,isMain,UpdDate,rsrc_guid )");
                            InsertSQL.AppendLine("values (");
                            InsertSQL.AppendLine("@Id,@MasterId,@Sequ,@Step,@Weight,@TotalWeight,@Quantities,@isMain,@UpdDate,@rsrc_guid )");
                            dbSQL.addParam("Id", ZBid);
                            dbSQL.addParam("MasterId", id);
                            dbSQL.addParam("Sequ", "1");
                            dbSQL.addParam("Step", PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["ProcSub_Name"].ToString());
                            dbSQL.addParam("Weight", "0");
                            dbSQL.addParam("TotalWeight", "0");
                            dbSQL.addParam("Quantities", "0");
                            dbSQL.addParam("isMain", "0");
                            dbSQL.addParam("UpdDate", DateTime.Now);
                            dbSQL.addParam("rsrc_guid", "00000000-0000-0000-0000-000000000000");
                            dbSQL.doSQL(InsertSQL.ToString());
                        }

                    }
                }
                else
                {
                    Guid id = Guid.NewGuid();
                    InsertSQL.Clear();
                    InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine( ");
                    InsertSQL.AppendLine("Id,Code,Title,BizAreaId,Sequ,Status,RegHumId,RegHumName,RegDate, ");
                    InsertSQL.AppendLine("RegPosiId,RegDeptId,EpsProjId,RecycleHumId,UpdHumId,UpdHumName,UpdDate,ApprHumId,OwnProjId,OwnProjName ) ");
                    InsertSQL.AppendLine("values ( ");
                    InsertSQL.AppendLine("@Id,@Code,@Title,@BizAreaId,@Sequ,@Status,@RegHumId,@RegHumName,@RegDate, ");
                    InsertSQL.AppendLine("@RegPosiId,@RegDeptId,@EpsProjId,@RecycleHumId,@UpdHumId,@UpdHumName,@UpdDate,@ApprHumId,@OwnProjId,@OwnProjName ) ");
                    dbSQL.addParam("Id", id);
                    dbSQL.addParam("Code", GetMaxXH());
                    dbSQL.addParam("Title", proc_name);
                    dbSQL.addParam("BizAreaId", "00000000-0000-0000-0000-00000000000A");
                    dbSQL.addParam("Sequ", GetMaxXH());
                    dbSQL.addParam("Status", "0");
                    dbSQL.addParam("RegHumId", "AD000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("RegHumName", "系统管理员");
                    dbSQL.addParam("RegDate", DateTime.Now);
                    dbSQL.addParam("RegPosiId", "00000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("RegDeptId", "00000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("EpsProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                    dbSQL.addParam("RecycleHumId", "AD000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("UpdHumId", "AD000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("UpdHumName", "系统管理员");
                    dbSQL.addParam("UpdDate", DateTime.Now);
                    dbSQL.addParam("ApprHumId", "00000000-0000-0000-0000-000000000000");
                    dbSQL.addParam("OwnProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                    dbSQL.addParam("OwnProjName", "江阴靖江长江隧道工程");
                    dbSQL.doSQL(InsertSQL.ToString());


                    sSQL.Clear();
                    sSQL.AppendLine("select proc_name,proc_guid from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid) ");//存在工序的构件
                    PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
                    if (PLN_taskproc.Tables[0].Rows.Count > 0)
                    {
                        sBOQ.Clear();
                        sBOQ.AppendLine("select ProcSub_Name from PS_PLN_TaskProc_Sub where proc_guid=@proc_guid ");
                        dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[0]["proc_guid"].ToString());
                        PS_PLN_TaskProc_Sub = dbSQL.getDataSet(sBOQ.ToString());
                        for (int u = 0; u < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; u++)
                        {
                            Guid ZBid = Guid.NewGuid();
                            InsertSQL.Clear();
                            InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine_Dtl (");
                            InsertSQL.AppendLine("Id,MasterId,Sequ,Step,Weight,TotalWeight,Quantities,isMain,UpdDate,rsrc_guid )");
                            InsertSQL.AppendLine("values (");
                            InsertSQL.AppendLine("@Id,@MasterId,@Sequ,@Step,@Weight,@TotalWeight,@Quantities,@isMain,@UpdDate,@rsrc_guid )");
                            dbSQL.addParam("Id", ZBid);
                            dbSQL.addParam("MasterId", id);
                            dbSQL.addParam("Sequ", "1");
                            dbSQL.addParam("Step", PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["ProcSub_Name"].ToString());
                            dbSQL.addParam("Weight", "0");
                            dbSQL.addParam("TotalWeight", "0");
                            dbSQL.addParam("Quantities", "0");
                            dbSQL.addParam("isMain", "0");
                            dbSQL.addParam("UpdDate", DateTime.Now);
                            dbSQL.addParam("rsrc_guid", "00000000-0000-0000-0000-000000000000");
                            dbSQL.doSQL(InsertSQL.ToString());
                        }
                    }
                }

            }
        }


        public static void InsertGXMB1()
        {
            SqlDataBase dbSQL = new SqlDataBase();
            StringBuilder sSQL = new StringBuilder();
            StringBuilder sBOQ = new StringBuilder();
            StringBuilder InsertSQL = new StringBuilder();
            sSQL.Clear();
            sSQL.AppendLine("select ID from PS_MDM_MeasureModelDefine ");
            DataSet dsSQL = dbSQL.getDataSet(sSQL.ToString());
            if (dsSQL.Tables[0].Rows.Count > 0)
            {
                Boolean SFInsert = false;
                for (int i = 0; i < dsSQL.Tables[0].Rows.Count; i++)//循环主表，根据主表的id关联子表的masterid，查询出子表的结构
                {
                    List<string> stepList = new List<string>();
                    string masterid = dsSQL.Tables[0].Rows[i]["ID"].ToString();
                    sSQL.Clear();
                    sSQL.AppendLine("select step from PS_MDM_MeasureModelDefine_Dtl where masterid=@masterid ");
                    dbSQL.addParam("masterid", masterid);
                    DataSet dsSQL1 = dbSQL.getDataSet(sSQL.ToString());
                    for (int j = 0; j < dsSQL1.Tables[0].Rows.Count; j++)
                    {
                        stepList.Add(dsSQL1.Tables[0].Rows[j]["step"].ToString());
                    }
                    
                    Boolean SFXYGZB = false;
                    sSQL.Clear();
                    sSQL.AppendLine("select proc_guid from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid) ");//存在工序的构件
                    DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
                    for (int k = 0; k < PLN_taskproc.Tables[0].Rows.Count; k++)
                    {
                        if (SFXYGZB)//如果已经有相同的了，则不需要再循环判断
                        {
                            break;
                        }
                        sBOQ.Clear();
                        sBOQ.AppendLine("select ProcSub_Name from PS_PLN_TaskProc_Sub where proc_guid=@proc_guid ");
                        dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[k]["proc_guid"].ToString());
                        DataSet PS_PLN_TaskProc_Sub = dbSQL.getDataSet(sBOQ.ToString());
                        Boolean SFTCBC = false;
                        for (int u = 0; u < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; u++)
                        {
                            if (SFXYGZB)//如果已经有相同的了，则不需要再循环判断
                            {
                                break;
                            }
                            if (SFTCBC)
                            {
                                break;
                            }
                            Boolean flag = false;
                            if (stepList.Count == PS_PLN_TaskProc_Sub.Tables[0].Rows.Count)
                            {
                                for (int y = 0; y < stepList.Count; y++)
                                {
                                    if (PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["ProcSub_Name"].ToString().Equals(stepList[y].ToString()))
                                    {
                                        flag = true;
                                    }
                                    if (y == stepList.Count - 1)//如果对比后没有一个相同的，则
                                    {
                                        if (!flag)
                                        {
                                            SFInsert = true;
                                            SFTCBC = true;
                                            break;
                                        }
                                        else//说明存在相同的，直接开始循环下一个子表
                                        {
                                            SFXYGZB = true;
                                            SFInsert = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SFInsert = true;
                                SFTCBC = true;
                                break;
                            }                       
                        }

                        if (k == PLN_taskproc.Tables[0].Rows.Count - 1 && !SFXYGZB)
                        {
                            if (SFInsert)
                            {
                                Guid id = Guid.NewGuid();
                                InsertSQL.Clear();
                                InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine( ");
                                InsertSQL.AppendLine("Id,Code,Title,BizAreaId,Sequ,Status,RegHumId,RegHumName,RegDate, ");
                                InsertSQL.AppendLine("RegPosiId,RegDeptId,EpsProjId,RecycleHumId,UpdHumId,UpdHumName,UpdDate,ApprHumId,OwnProjId,OwnProjName ) ");
                                InsertSQL.AppendLine("values ( ");
                                InsertSQL.AppendLine("@Id,@Code,@Title,@BizAreaId,@Sequ,@Status,@RegHumId,@RegHumName,@RegDate, ");
                                InsertSQL.AppendLine("@RegPosiId,@RegDeptId,@EpsProjId,@RecycleHumId,@UpdHumId,@UpdHumName,@UpdDate,@ApprHumId,@OwnProjId,@OwnProjName ) ");
                                dbSQL.addParam("Id", id);
                                dbSQL.addParam("Code", "1");
                                dbSQL.addParam("Title", "测试");
                                dbSQL.addParam("BizAreaId", "00000000-0000-0000-0000-00000000000A");
                                dbSQL.addParam("Sequ", "1");
                                dbSQL.addParam("Status", "0");
                                dbSQL.addParam("RegHumId", "AD000000-0000-0000-0000-000000000000");
                                dbSQL.addParam("RegHumName", "系统管理员");
                                dbSQL.addParam("RegDate", DateTime.Now);
                                dbSQL.addParam("RegPosiId", "00000000-0000-0000-0000-000000000000");
                                dbSQL.addParam("RegDeptId", "00000000-0000-0000-0000-000000000000");
                                dbSQL.addParam("EpsProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                                dbSQL.addParam("RecycleHumId", "AD000000-0000-0000-0000-000000000000");
                                dbSQL.addParam("UpdHumId", "AD000000-0000-0000-0000-000000000000");
                                dbSQL.addParam("UpdHumName", "系统管理员");
                                dbSQL.addParam("UpdDate", DateTime.Now);
                                dbSQL.addParam("ApprHumId", "00000000-0000-0000-0000-000000000000");
                                dbSQL.addParam("OwnProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                                dbSQL.addParam("OwnProjName", "江阴靖江长江隧道工程");
                                dbSQL.doSQL(InsertSQL.ToString());

                                for (int u = 0; u < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; u++)
                                {
                                    Guid ZBid = Guid.NewGuid();
                                    InsertSQL.Clear();
                                    InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine_Dtl (");
                                    InsertSQL.AppendLine("Id,MasterId,Sequ,Step,Weight,TotalWeight,Quantities,isMain,UpdDate,rsrc_guid )");
                                    InsertSQL.AppendLine("values (");
                                    InsertSQL.AppendLine("@Id,@MasterId,@Sequ,@Step,@Weight,@TotalWeight,@Quantities,@isMain,@UpdDate,@rsrc_guid )");
                                    dbSQL.addParam("Id", ZBid);
                                    dbSQL.addParam("MasterId", id);
                                    dbSQL.addParam("Sequ", "1");
                                    dbSQL.addParam("Step", PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["ProcSub_Name"].ToString());
                                    dbSQL.addParam("Weight", "0");
                                    dbSQL.addParam("TotalWeight", "0");
                                    dbSQL.addParam("Quantities", "0");
                                    dbSQL.addParam("isMain", "0");
                                    dbSQL.addParam("UpdDate", DateTime.Now);
                                    dbSQL.addParam("rsrc_guid", "00000000-0000-0000-0000-000000000000");
                                    dbSQL.doSQL(InsertSQL.ToString());
                                }

                                }
                            }
                        }
                    }                 
            }
            else//如果模板中不存在记录，则直接插入
            {
                Guid id = Guid.NewGuid();
                InsertSQL.Clear();
                InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine( ");
                InsertSQL.AppendLine("Id,Code,Title,BizAreaId,Sequ,Status,RegHumId,RegHumName,RegDate, ");
                InsertSQL.AppendLine("RegPosiId,RegDeptId,EpsProjId,RecycleHumId,UpdHumId,UpdHumName,UpdDate,ApprHumId,OwnProjId,OwnProjName ) ");
                InsertSQL.AppendLine("values ( ");
                InsertSQL.AppendLine("@Id,@Code,@Title,@BizAreaId,@Sequ,@Status,@RegHumId,@RegHumName,@RegDate, ");
                InsertSQL.AppendLine("@RegPosiId,@RegDeptId,@EpsProjId,@RecycleHumId,@UpdHumId,@UpdHumName,@UpdDate,@ApprHumId,@OwnProjId,@OwnProjName ) ");
                dbSQL.addParam("Id", id);
                dbSQL.addParam("Code","1");
                dbSQL.addParam("Title","测试");
                dbSQL.addParam("BizAreaId", "00000000-0000-0000-0000-00000000000A");
                dbSQL.addParam("Sequ","1");
                dbSQL.addParam("Status","0");
                dbSQL.addParam("RegHumId", "AD000000-0000-0000-0000-000000000000");
                dbSQL.addParam("RegHumName", "系统管理员");
                dbSQL.addParam("RegDate",DateTime.Now);
                dbSQL.addParam("RegPosiId", "00000000-0000-0000-0000-000000000000");
                dbSQL.addParam("RegDeptId", "00000000-0000-0000-0000-000000000000");
                dbSQL.addParam("EpsProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                dbSQL.addParam("RecycleHumId", "AD000000-0000-0000-0000-000000000000");
                dbSQL.addParam("UpdHumId", "AD000000-0000-0000-0000-000000000000");
                dbSQL.addParam("UpdHumName", "系统管理员");
                dbSQL.addParam("UpdDate", DateTime.Now);
                dbSQL.addParam("ApprHumId", "00000000-0000-0000-0000-000000000000");
                dbSQL.addParam("OwnProjId", "B5C3A1AA-F030-4E57-B4D9-F164660D5CD4");
                dbSQL.addParam("OwnProjName", "江阴靖江长江隧道工程");
                dbSQL.doSQL(InsertSQL.ToString());


                sSQL.Clear();
                sSQL.AppendLine("select proc_guid from PLN_taskproc A where exists(select 1 from PS_PLN_TaskProc_Sub B where A.proc_guid=B.proc_guid) ");//存在工序的构件
                DataSet PLN_taskproc = dbSQL.getDataSet(sSQL.ToString());
                if(PLN_taskproc.Tables[0].Rows.Count>0)
                {
                    sBOQ.Clear();
                    sBOQ.AppendLine("select ProcSub_Name from PS_PLN_TaskProc_Sub where proc_guid=@proc_guid ");
                    dbSQL.addParam("proc_guid", PLN_taskproc.Tables[0].Rows[0]["proc_guid"].ToString());
                    DataSet PS_PLN_TaskProc_Sub = dbSQL.getDataSet(sBOQ.ToString());
                    for (int u = 0; u < PS_PLN_TaskProc_Sub.Tables[0].Rows.Count; u++)
                    {
                        Guid ZBid = Guid.NewGuid();
                        InsertSQL.Clear();
                        InsertSQL.AppendLine("insert into PS_MDM_MeasureModelDefine_Dtl (");
                        InsertSQL.AppendLine("Id,MasterId,Sequ,Step,Weight,TotalWeight,Quantities,isMain,UpdDate,rsrc_guid )");
                        InsertSQL.AppendLine("values (");
                        InsertSQL.AppendLine("@Id,@MasterId,@Sequ,@Step,@Weight,@TotalWeight,@Quantities,@isMain,@UpdDate,@rsrc_guid )");
                        dbSQL.addParam("Id", ZBid);
                        dbSQL.addParam("MasterId", id);
                        dbSQL.addParam("Sequ", "1");
                        dbSQL.addParam("Step", PS_PLN_TaskProc_Sub.Tables[0].Rows[u]["ProcSub_Name"].ToString());
                        dbSQL.addParam("Weight", "0");
                        dbSQL.addParam("TotalWeight", "0");
                        dbSQL.addParam("Quantities", "0");
                        dbSQL.addParam("isMain", "0");
                        dbSQL.addParam("UpdDate", DateTime.Now);
                        dbSQL.addParam("rsrc_guid", "00000000-0000-0000-0000-000000000000");
                        dbSQL.doSQL(InsertSQL.ToString());
                    }
                }

            }
            
        }


        public static void test()
        {
            string s = DateTime.Now.AddMonths(0 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01");
            DateTime s1 = DateTime.Parse(DateTime.Now.AddMonths(3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1);
            string s2= DateTime.Now.AddMonths(-3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01");
            string s3= DateTime.Parse(DateTime.Now.AddMonths(0 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            int da= s1.Month;

           int i= GetQuarterNum(DateTime.Now);
        }

        /// <summary>
        /// C#获取指定日期时间是当前年度的第几个季度
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetQuarterNum(DateTime dt)
        {
            int year = dt.Year;
            int jd;
            DateTime dt0 = new DateTime(year, 1, 1);
            DateTime dt1 = new DateTime(year, 4, 1);
            DateTime dt2 = new DateTime(year, 7, 1);
            DateTime dt3 = new DateTime(year, 10, 1);
            if (dt.CompareTo(dt1) < 0)
                jd = 1;
            else if (dt.CompareTo(dt2) < 0)
                jd = 2;
            else if (dt.CompareTo(dt3) < 0)
                jd = 3;
            else
                jd = 4;
            return jd;
        }



        public static void TestThread()
        {
            //创建无参的线程
            Thread thread1 = new Thread(new ThreadStart(UpdateGXWet));

            Thread thread2 = new Thread(new ThreadStart(UpdateGXWet2));

            //调用Start方法执行线程
            thread1.Start();

            thread2.Start();
        }
    }
}
