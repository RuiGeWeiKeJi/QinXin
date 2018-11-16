using System . Data;
using System . Text;
using StudentMgr;
using System . Collections;
using System;
using System . Data . SqlClient;
using System . Collections . Generic;
using System . Linq;

namespace SiftingDataBll . Dao
{
    public class MainDao
    {
        /// <summary>
        /// 获取单头数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableQuery ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT IBB001,IBA003,DFA002,IBB003,IBB004,IBB961 FROM QIXDEL A INNER JOIN DCSIBB B ON A.DEL017=B.IBB001 INNER JOIN DCSIBA C ON B.IBB001=C.IBA001  INNER JOIN TPADFA D ON DFA001=IBA004 WHERE DEL014 = 1" );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取主件品号信息
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public DataTable getTableQueryPin ( string orderNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT IBB002,IBB003,IBB004,IBB041,IBB961,CONVERT(FLOAT,IBB006) IBB006,IBB005,CONVERT(FLOAT,IBB980) IBB980,CONVERT(FLOAT,IBB981) IBB981 FROM QIXDEL A INNER JOIN DCSIBB B ON A.DEL017=B.IBB001 WHERE DEL014=1 AND IBB001='{0}'" ,orderNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 获取品号
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DEA001,DEA002,DEA057,DEA003 FROM TPADEA" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取配方单号
        /// </summary>
        /// <returns></returns>
        public DataTable getTableOrder ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT DEL015,DEL017,DEL016 FROM QIXDEL A INNER JOIN DCSIBB B ON A.DEL017=B.IBB001 WHERE DEL014=1" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取子表
        /// </summary>
        /// <param name="gdNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,DEL001,DEL002,DEL003,DEL004,DEL005,DEL006,DEL007,DEL008,DEL009,DEL010,DEL011,DEL012,DEL013,DEL014,DEL016,DEL017,DEL018 FROM QIXDEL WHERE  {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 根据子件品号获取批号
        /// </summary>
        /// <param name="pNum"></param>
        /// <returns></returns>
        public DataTable getTablePiHao ( string pNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LOA011,LOA002,LOA003 FROM JSKLOA WHERE LOA001='{0}'" ,pNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <returns></returns>
        public bool Save ( DataTable table ,string oderNum,string remark   )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            SiftingDataEntity . MainEntity model = new SiftingDataEntity . MainEntity ( );
            model . DEL013 = remark;
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                model . DEL011 = oderNum;
                model . DEL001 = table . Rows [ i ] [ "DEL001" ] . ToString ( );
                model . DEL002 = table . Rows [ i ] [ "DEL002" ] . ToString ( );
                model . DEL003 = table . Rows [ i ] [ "DEL003" ] . ToString ( );
                model . DEL004 = table . Rows [ i ] [ "DEL004" ] . ToString ( );
                model . DEL005 = table . Rows [ i ] [ "DEL005" ] . ToString ( );
                model . DEL006 = table . Rows [ i ] [ "DEL006" ] . ToString ( );
                model . DEL007 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) );
                model . DEL010 = table . Rows [ i ] [ "DEL010" ] . ToString ( );/* string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) );*/
                model . DEL012 = table . Rows [ i ] [ "DEL012" ] . ToString ( );
                Add ( SQLString ,strSql ,model );
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void Add ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into QIXDEL(" );
            strSql . Append ( "DEL001,DEL002,DEL003,DEL004,DEL005,DEL006,DEL007,DEL008,DEL009,DEL010,DEL011,DEL012,DEL013)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@DEL001,@DEL002,@DEL003,@DEL004,@DEL005,@DEL006,@DEL007,@DEL008,@DEL009,@DEL010,@DEL011,@DEL012,@DEL013)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DEL001", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL002", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL003", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL004", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL005", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL006", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL007", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL008", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL009", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL010", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL011", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL012", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL013", SqlDbType.NVarChar,250)
            };
            parameters [ 0 ] . Value = model . DEL001;
            parameters [ 1 ] . Value = model . DEL002;
            parameters [ 2 ] . Value = model . DEL003;
            parameters [ 3 ] . Value = model . DEL004;
            parameters [ 4 ] . Value = model . DEL005;
            parameters [ 5 ] . Value = model . DEL006;
            parameters [ 6 ] . Value = model . DEL007;
            parameters [ 7 ] . Value = model . DEL008;
            parameters [ 8 ] . Value = model . DEL009;
            parameters [ 9 ] . Value = model . DEL010;
            parameters [ 10 ] . Value = model . DEL011;
            parameters [ 11 ] . Value = model . DEL012;
            parameters [ 12 ] . Value = model . DEL013;

            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Update ( DataTable table ,string oderNum ,List<int> idxList ,string remark )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            SiftingDataEntity . MainEntity model = new SiftingDataEntity . MainEntity ( );
            model . DEL013 = remark;
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                model . DEL011 = oderNum;
                model . DEL001 = table . Rows [ i ] [ "DEL001" ] . ToString ( );
                model . DEL002 = table . Rows [ i ] [ "DEL002" ] . ToString ( );
                model . DEL003 = table . Rows [ i ] [ "DEL003" ] . ToString ( );
                model . DEL004 = table . Rows [ i ] [ "DEL004" ] . ToString ( );
                model . DEL005 = table . Rows [ i ] [ "DEL005" ] . ToString ( );
                model . DEL006 = table . Rows [ i ] [ "DEL006" ] . ToString ( );
                model . DEL007 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) );
                model . DEL010 = table . Rows [ i ] [ "DEL010" ] . ToString ( ); /*string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) );*/
                model . id = string . IsNullOrEmpty ( table . Rows [ i ] [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ i ] [ "idx" ] . ToString ( ) );
                model . DEL012 = table . Rows [ i ] [ "DEL012" ] . ToString ( );
                if ( model . id == 0 )
                    Add ( SQLString ,strSql ,model );
                else
                    Edit ( SQLString ,strSql ,model );
            }

            if ( idxList != null && idxList . Count > 0 )
            {
                foreach ( int i in idxList )
                {
                    model . id = i;
                    if ( model . id > 0 )
                        Remove ( SQLString ,strSql ,model );
                }
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void Edit ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update QIXDEL set " );
            strSql . Append ( "DEL001=@DEL001," );
            strSql . Append ( "DEL002=@DEL002," );
            strSql . Append ( "DEL003=@DEL003," );
            strSql . Append ( "DEL004=@DEL004," );
            strSql . Append ( "DEL005=@DEL005," );
            strSql . Append ( "DEL006=@DEL006," );
            strSql . Append ( "DEL007=@DEL007," );
            strSql . Append ( "DEL008=@DEL008," );
            strSql . Append ( "DEL009=@DEL009," );
            strSql . Append ( "DEL010=@DEL010," );
            strSql . Append ( "DEL011=@DEL011," );
            strSql . Append ( "DEL012=@DEL012," );
            strSql . Append ( "DEL013=@DEL013 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DEL001", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL002", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL003", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL004", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL005", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL006", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL007", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL008", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL009", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL010", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL011", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL012", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL013", SqlDbType.NVarChar,250),
                    new SqlParameter("@idx", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . DEL001;
            parameters [ 1 ] . Value = model . DEL002;
            parameters [ 2 ] . Value = model . DEL003;
            parameters [ 3 ] . Value = model . DEL004;
            parameters [ 4 ] . Value = model . DEL005;
            parameters [ 5 ] . Value = model . DEL006;
            parameters [ 6 ] . Value = model . DEL007;
            parameters [ 7 ] . Value = model . DEL008;
            parameters [ 8 ] . Value = model . DEL009;
            parameters [ 9 ] . Value = model . DEL010;
            parameters [ 10 ] . Value = model . DEL011;
            parameters [ 11 ] . Value = model . DEL012;
            parameters [ 12 ] . Value = model . DEL013;
            parameters [ 13 ] . Value = model . id;

            SQLString . Add ( strSql ,parameters );
        }

        void Remove ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "delete from QIXDEL " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . id;

            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 工单是否审核
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool ExistsSH ( string orderNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM SGMRAA WHERE RAA024='T' AND RAA001='{0}'" ,orderNum );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool Delete ( string orderNum ,string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "delete from QIXDEL WHERE DEL017='{0}' AND DEL016='{1}'" ,orderNum ,num );

            int rows= SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
            return rows > 0 ? true : false;
        }

        /// <summary>
        /// 插入工单单身数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SaveOrder ( string oddNum )
        {
            bool isOk = true;
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM SGMRAA WHERE RAA001='{0}'" ,oddNum );
            if ( SqlHelper . Exists ( strSql . ToString ( ) ) == false )
                //表示易助订单没有单头数据
                return -1;
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT RAB002,RAB003 FROM SGMRAB WHERE RAB001='{0}'" ,oddNum );
            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt == null || dt . Rows . Count < 1 )
                isOk = false;

            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL011 RAB001,DEL001 RAB003,DEL002 RAB004,DEL003 RAB020,DEL004 RAB005,SUM(DEL007*DEL008+DEL009) RAB007,SUM(DEL007) RAB980,SUM(DEL008) RAB981,SUM(DEL010) RAB982 FROM QIXDEL WHERE DEL011='{0}' GROUP BY DEL011,DEL001,DEL002,DEL004,DEL003" ,oddNum );
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                //表示本单号在本程序没有单身数据
                return -2;
            
            List<string> strList = new List<string> ( );

            SiftingDataEntity . OrderWorkEntity model = new SiftingDataEntity . OrderWorkEntity ( );
            model . RAB001 = oddNum;
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                model . RAB003 = table . Rows [ i ] [ "RAB003" ] . ToString ( );
                if ( isOk )
                {
                    model . RAB002 = dt . Compute ( "MAX(RAB002)" ,null ) . ToString ( );
                    if ( strList . Contains ( model . RAB002 ) )
                    {
                        model . RAB002 = strList . Max ( );
                        model . RAB002 = ( Convert . ToInt32 ( model . RAB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                        strList . Add ( model . RAB002 );
                    }
                    else
                    {
                        strList . Add ( model . RAB002 );
                        model . RAB002 = ( Convert . ToInt32 ( model . RAB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                        strList . Add ( model . RAB002 );
                    }
                }
                else
                {
                    model . RAB002 = i . ToString ( ) . PadLeft ( 3 ,'0' );
                    if ( strList . Contains ( model . RAB002 ) )
                    {
                        model . RAB002 = strList . Max ( );
                        model . RAB002 = ( Convert . ToInt32 ( model . RAB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                        strList . Add ( model . RAB002 );
                    }
                    else
                        strList . Add ( model . RAB002 );
                }
                model . RAB004 = table . Rows [ i ] [ "RAB004" ] . ToString ( );
                model . RAB005 = table . Rows [ i ] [ "RAB005" ] . ToString ( );
                model . RAB007 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB007" ] . ToString ( ) );
                model . RAB980 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB980" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB980" ] . ToString ( ) );
                model . RAB981 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB981" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB981" ] . ToString ( ) );
                model . RAB982 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB982" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB982" ] . ToString ( ) );
                model . RAB020 = table . Rows [ i ] [ "RAB020" ] . ToString ( );

                if ( dt . Select ( "RAB003='" + model . RAB003 + "'" ) . Length < 1 )
                    AddOrder ( SQLString ,strSql ,model );
            }

            if ( SqlHelper . ExecuteSqlTran ( SQLString ) )
                return 1;
            else
                return 2;
        }

        void AddOrder ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . OrderWorkEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO SGMRAB (" );
            strSql . Append ( "RAB001,RAB002,RAB003,RAB004,RAB005,RAB007,RAB020,RAB980,RAB981,RAB982) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@RAB001,@RAB002,@RAB003,@RAB004,@RAB005,@RAB007,@RAB020,@RAB980,@RAB981,@RAB982) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@RAB001",SqlDbType.NVarChar),
                new SqlParameter("@RAB002",SqlDbType.NVarChar),
                new SqlParameter("@RAB003",SqlDbType.NVarChar),
                new SqlParameter("@RAB004",SqlDbType.NVarChar),
                new SqlParameter("@RAB005",SqlDbType.NVarChar),
                new SqlParameter("@RAB007",SqlDbType.Decimal),
                new SqlParameter("@RAB020",SqlDbType.NVarChar),
                new SqlParameter("@RAB980",SqlDbType.Decimal),
                new SqlParameter("@RAB981",SqlDbType.Decimal),
                new SqlParameter("@RAB982",SqlDbType.Decimal)
            };
            parameter [ 0 ] . Value = model . RAB001;
            parameter [ 1 ] . Value = model . RAB002;
            parameter [ 2 ] . Value = model . RAB003;
            parameter [ 3 ] . Value = model . RAB004;
            parameter [ 4 ] . Value = model . RAB005;
            parameter [ 5 ] . Value = model . RAB007;
            parameter [ 6 ] . Value = model . RAB020;
            parameter [ 7 ] . Value = model . RAB980;
            parameter [ 8 ] . Value = model . RAB981;
            parameter [ 9 ] . Value = model . RAB982;
            SQLString . Add ( strSql ,parameter );
        }

        /// <summary>
        /// 生成领料单  第二次覆盖上次的
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SavePick ( string oddNum ,string num )
        {
            ArrayList SQLString = new ArrayList ( );
            StringBuilder strSql = new StringBuilder ( );

            DataTable table;
            SiftingDataEntity . OrderPickEntity _header = new SiftingDataEntity . OrderPickEntity ( );
            _header . LIA004 = "C01";
            _header . LIA005 = "004";
            _header . LIA960 = oddNum;
            _header . LIA964 = num;
            _header . LIA007 = "F";
            _header . LIA013 = "1";
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL013 LIA962 FROM QIXDEL WHERE DEL017='{0}' AND DEL016='{1}'" ,oddNum ,num );
            table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                _header . LIA962 = string . Empty;
            else
                _header . LIA962 = table . Rows [ 0 ] [ "LIA962" ] . ToString ( );

            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL001 LIB003,DEL002 LIB004,DEL003 LIB024,DEL004 LIB005,DEL006 LIB006,DEL005 LIB019,DEL007*DEL008+DEL009 LIB008,DEL007 LIB982,DEL008 LIB983,DEL009 LIB984,DEL012 LIB960,IBB004 LIB964,IBB961 LIB961 FROM QIXDEL A INNER JOIN DCSIBB B ON A.DEL017=B.IBB001 WHERE DEL017='{0}' AND DEL016='{1}'" ,oddNum ,num );
            table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                //表示本地没有数据
                return -1;

            DataTable tableOne;
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM JSKLIA WHERE LIA960='{0}' AND LIA964='{1}'" ,oddNum ,num );
            tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( !SqlHelper . Exists ( strSql . ToString ( ) ) )
            {
                _header . LIA001 = getOddNumPick ( );
                AddLIA ( SQLString ,_header );
                
                SiftingDataEntity . OrderPickBodyEntity _body = new SiftingDataEntity . OrderPickBodyEntity ( );
                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    _body . LIB001 = _header . LIA001;
                    _body . LIB002 = ( i + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                    _body . LIB003 = table . Rows [ i ] [ "LIB003" ] . ToString ( );
                    _body . LIB004 = table . Rows [ i ] [ "LIB004" ] . ToString ( );
                    _body . LIB005 = table . Rows [ i ] [ "LIB005" ] . ToString ( );
                    _body . LIB006 = table . Rows [ i ] [ "LIB006" ] . ToString ( );
                    _body . LIB007 = -1;
                    _body . LIB008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) );
                    _body . LIB019 = table . Rows [ i ] [ "LIB019" ] . ToString ( );
                    _body . LIB020 = 1;
                    _body . LIB021 = 1;
                    _body . LIB022 = _body . LIB005;
                    _body . LIB024 = table . Rows [ i ] [ "LIB024" ] . ToString ( );
                    _body . LIB023 = _body . LIB008;
                    _body . LIB960 = table . Rows [ i ] [ "LIB960" ] . ToString ( );
                    _body . LIB961 = table . Rows [ i ] [ "LIB961" ] . ToString ( );
                    _body . LIB964 = table . Rows [ i ] [ "LIB964" ] . ToString ( );
                    _body . LIB984 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) );
                    _body . LIB982 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) );
                    _body . LIB983 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) );

                    AddPick ( SQLString ,strSql ,_body );
                }
            }
            else
            {
                _header . LIA001 = tableOne . Rows [ 0 ] [ "LIA001" ] . ToString ( );
                EditLIA ( SQLString ,_header );

                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT LIB003,LIB006,LIB019,LIB002 FROM JSKLIB WHERE LIB001='{0}'" ,_header . LIA001 );
                DataTable tableTwo = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                SiftingDataEntity . OrderPickBodyEntity _body = new SiftingDataEntity . OrderPickBodyEntity ( );
                List<string> codeList = new List<string> ( );
                string code = string . Empty;
                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    _body . LIB001 = _header . LIA001;
                    _body . LIB002 = ( i + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                    _body . LIB003 = table . Rows [ i ] [ "LIB003" ] . ToString ( );
                    _body . LIB004 = table . Rows [ i ] [ "LIB004" ] . ToString ( );
                    _body . LIB005 = table . Rows [ i ] [ "LIB005" ] . ToString ( );
                    _body . LIB006 = table . Rows [ i ] [ "LIB006" ] . ToString ( );
                    _body . LIB007 = -1;
                    _body . LIB008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) );
                    _body . LIB019 = table . Rows [ i ] [ "LIB019" ] . ToString ( );
                    _body . LIB020 = 1;
                    _body . LIB021 = 1;
                    _body . LIB022 = _body . LIB005;
                    _body . LIB024 = table . Rows [ i ] [ "LIB024" ] . ToString ( );
                    _body . LIB023 = _body . LIB008;
                    _body . LIB960 = table . Rows [ i ] [ "LIB960" ] . ToString ( );
                    _body . LIB961 = table . Rows [ i ] [ "LIB961" ] . ToString ( );
                    _body . LIB964 = table . Rows [ i ] [ "LIB964" ] . ToString ( );
                    _body . LIB984 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) );
                    _body . LIB982 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) );
                    _body . LIB983 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) );

                    if ( tableTwo != null && tableTwo . Rows . Count > 0 )
                    {
                        if ( tableTwo . Select ( "LIB003='" + _body . LIB003 + "' AND LIB006='" + _body . LIB006 + "' AND LIB019='" + _body . LIB019 + "'" ) . Length > 0 )
                        {
                            _body . LIB002 = tableTwo . Select ( "LIB003='" + _body . LIB003 + "' AND LIB006='" + _body . LIB006 + "' AND LIB019='" + _body . LIB019 + "'" ) [ 0 ] [ "LIB002" ] . ToString ( );
                            EditLIB ( SQLString ,_body );
                        }
                        else
                        {
                            _body . LIB002 = tableTwo . Compute ( "MAX(LIB002)" ,null ) . ToString ( );
                            _body . LIB002 = ( Convert . ToInt32 ( _body . LIB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                            if ( codeList . Contains ( _body . LIB002 ) )
                                code = codeList . Max ( );
                            else
                                code = string . Empty;
                            if ( code != string . Empty && Convert . ToInt32 ( code ) > Convert . ToInt32 ( _body . LIB002 ) )
                                _body . LIB002 = ( Convert . ToInt32 ( code ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                            AddPick ( SQLString ,strSql ,_body );
                        }
                    }
                    else
                        AddPick ( SQLString ,strSql ,_body );

                    codeList . Add ( _body . LIB002 );
                }
            }


            if ( SqlHelper . ExecuteSqlTran ( SQLString ) )
                return 1;
            else
                return 2;
        }

        /// <summary>
        /// 生成领料单  不覆盖上次的
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int SavePickOne ( string oddNum ,string num ,string piNum,string numOfOrder )
        {
            ArrayList SQLString = new ArrayList ( );
            StringBuilder strSql = new StringBuilder ( );

            SiftingDataEntity . OrderPickBodyEntity _body = new SiftingDataEntity . OrderPickBodyEntity ( );
            //_body . LIB961 = piNum;
            //_body . LIB964 = num;

            DataTable table;
            SiftingDataEntity . OrderPickEntity _header = new SiftingDataEntity . OrderPickEntity ( );
            _header . LIA004 = "C01";
            _header . LIA005 = "004";
            _header . LIA960 = oddNum +"-"+ numOfOrder;
            _header . LIA964 = num;
            _header . LIA007 = "F";
            _header . LIA013 = "1";
            _header . LIA963 = piNum;
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL013 LIA962 FROM QIXDEL WHERE DEL017='{0}' AND DEL016='{1}' AND DEL018='{2}'" ,oddNum ,num ,numOfOrder );
            table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                _header . LIA011 = string . Empty;
            else
                _header . LIA011 = table . Rows [ 0 ] [ "LIA962" ] . ToString ( );

            strSql = new StringBuilder ( );
            //strSql . AppendFormat ( "SELECT DEL001 LIB003,DEL002 LIB004,DEL003 LIB024,DEL004 LIB005,DEL006 LIB006,DEL005 LIB019,DEL007*DEL008+DEL009 LIB008,DEL007 LIB982,DEL008 LIB983,DEL009 LIB984,DEL012 LIB960,IBB004 LIB964,IBB961 LIB961 FROM QIXDEL A INNER JOIN DCSIBB B ON A.DEL017=B.IBB001 WHERE DEL017='{0}' AND DEL016='{1}'" ,oddNum ,num );
            strSql . AppendFormat ( "SELECT DEL001 LIB003,DEL002 LIB004,DEL003 LIB024,DEL004 LIB005,DEL006 LIB006,DEL005 LIB019,DEL007*DEL008+DEL009 LIB008,DEL007 LIB982,DEL008 LIB983,DEL009 LIB984,DEL012 LIB960 FROM QIXDEL WHERE DEL017='{0}' AND DEL016='{1}' AND DEL018='{2}'" ,oddNum ,num ,numOfOrder );
            table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                //表示本地没有数据
                return -1;

            DataTable tableOne;
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM JSKLIA WHERE LIA960='{0}' AND LIA964='{1}'" ,oddNum + "-" + numOfOrder ,num );
            if ( SqlHelper . Exists ( strSql . ToString ( ) ) )
                return -2;
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LIA001 FROM JSKLIA WHERE LIA960='{0}' AND LIA964='{1}'" ,oddNum + "-" + numOfOrder ,num );
            tableOne = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( !SqlHelper . Exists ( strSql . ToString ( ) ) )
            {
                _header . LIA001 = getOddNumPick ( );
                AddLIA ( SQLString ,_header );

                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    _body . LIB001 = _header . LIA001;
                    _body . LIB002 = ( i + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                    _body . LIB003 = table . Rows [ i ] [ "LIB003" ] . ToString ( );
                    _body . LIB004 = table . Rows [ i ] [ "LIB004" ] . ToString ( );
                    _body . LIB005 = table . Rows [ i ] [ "LIB005" ] . ToString ( );
                    _body . LIB006 = table . Rows [ i ] [ "LIB006" ] . ToString ( );
                    _body . LIB007 = -1;
                    _body . LIB008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) );
                    _body . LIB019 = table . Rows [ i ] [ "LIB019" ] . ToString ( );
                    _body . LIB020 = 1;
                    _body . LIB021 = 1;
                    _body . LIB022 = _body . LIB005;
                    _body . LIB024 = table . Rows [ i ] [ "LIB024" ] . ToString ( );
                    _body . LIB023 = _body . LIB008;
                    _body . LIB960 = table . Rows [ i ] [ "LIB960" ] . ToString ( );
                    //_body . LIB961 = table . Rows [ i ] [ "LIB961" ] . ToString ( );
                    //_body . LIB964 = table . Rows [ i ] [ "LIB964" ] . ToString ( );
                    _body . LIB984 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) );
                    _body . LIB982 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) );
                    _body . LIB983 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) );

                    AddPick ( SQLString ,strSql ,_body );
                }
            }
            else
                return -2;

            if ( SqlHelper . ExecuteSqlTran ( SQLString ) )
                return 1;
            else
                return 2;
        }

        void AddLIA ( ArrayList SQLString ,SiftingDataEntity . OrderPickEntity _header )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO JSKLIA (" );
            strSql . Append ( "LIA001,LIA003,LIA004,LIA005,LIA012,LIA960,LIA007,LIA013,LIA011,LIA964,LIA963) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{3}','{1}','{4}','F','{2}','{5}','{6}','{7}','{8}','{9}')" ,_header . LIA001 ,_header . LIA004 ,_header . LIA960 ,getTime ( ) . ToString ( "yyyyMMdd" ) ,_header . LIA005 ,_header . LIA007 ,_header . LIA013 ,_header . LIA011 ,_header . LIA964 ,_header . LIA963 );
            SQLString . Add ( strSql . ToString ( ) );
        }
        void AddPick ( ArrayList SQLString ,StringBuilder strSql ,SiftingDataEntity . OrderPickBodyEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO JSKLIB (" );
            strSql . Append ( "LIB001,LIB002,LIB003,LIB004,LIB005,LIB006,LIB008,LIB019,LIB982,LIB983,LIB984,LIB960,LIB007,LIB020,LIB021,LIB022,LIB023,LIB024) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}') " ,model . LIB001 ,model . LIB002 ,model . LIB003 ,model . LIB004 ,model . LIB005 ,model . LIB006 ,model . LIB008 ,model . LIB019 ,model . LIB982 ,model . LIB983 ,model . LIB984 ,model . LIB960 ,model . LIB007 ,model . LIB020 ,model . LIB021 ,model . LIB022 ,model . LIB023 ,model . LIB024  );
            SQLString . Add ( strSql . ToString ( ) );
        }

        void EditLIA ( ArrayList SQLString ,SiftingDataEntity . OrderPickEntity _header )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE JSKLIA SET " );
            strSql . AppendFormat ( "LIA011='{0}' " ,_header . LIA011 );
            strSql . AppendFormat ( "WHERE LIA001='{0}'" ,_header . LIA001 );
            SQLString . Add ( strSql . ToString ( ) );
        }
        void EditLIB ( ArrayList SQLString ,SiftingDataEntity . OrderPickBodyEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE JSKLIB SET " );
            strSql . AppendFormat ( "LIB003='{0}'," ,model . LIB003 );
            strSql . AppendFormat ( "LIB004='{0}'," ,model . LIB004 );
            strSql . AppendFormat ( "LIB005='{0}'," ,model . LIB005 );
            strSql . AppendFormat ( "LIB006='{0}'," ,model . LIB006 );
            strSql . AppendFormat ( "LIB008='{0}'," ,model . LIB008 );
            strSql . AppendFormat ( "LIB019='{0}'," ,model . LIB019 );
            strSql . AppendFormat ( "LIB982='{0}'," ,model . LIB982 );
            strSql . AppendFormat ( "LIB983='{0}'," ,model . LIB983 );
            strSql . AppendFormat ( "LIB984='{0}'," ,model . LIB984 );
            strSql . AppendFormat ( "LIB960='{0}'," ,model . LIB960 );
            strSql . AppendFormat ( "LIB007='{0}'," ,model . LIB007 );
            strSql . AppendFormat ( "LIB020='{0}'," ,model . LIB020 );
            strSql . AppendFormat ( "LIB021='{0}'," ,model . LIB021 );
            strSql . AppendFormat ( "LIB022='{0}'," ,model . LIB022 );
            strSql . AppendFormat ( "LIB023='{0}'," ,model . LIB023 );
            strSql . AppendFormat ( "LIB024='{0}'," ,model . LIB024 );
            strSql . AppendFormat ( "LIB961='{0}'," ,model . LIB961 );
            strSql . AppendFormat ( "LIB964='{0}' " ,model . LIB964 );
            strSql . AppendFormat ( "WHERE LIB001='{0}' AND LIB002='{1}'" ,model . LIB001 ,model . LIB002 );
            SQLString . Add ( strSql . ToString ( ) );
        }

        string getOddNumPick ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(LIA001) LIA001 FROM JSKLIA WHERE LIA001 LIKE 'CK{0}%'" ,getTime ( ) . ToString ( "yyyyMMdd" ) );
            
            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                string oddNum = dt . Rows [ 0 ] [ "LIA001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( oddNum ) )
                    return "CK" + getTime ( ) . ToString ( "yyyyMMdd" ) + "0001";
                else
                    return "CK" + ( Convert . ToInt64 ( oddNum . Substring ( 2 ,12 ) ) + 1 ) . ToString ( );
            }
            else
                return "CK" + getTime ( ) . ToString ( "yyyyMMdd" ) + "0001";
        }

        DateTime getTime ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT GETDATE() t;" );

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt != null && dt . Rows . Count < 1 )
                return string . IsNullOrEmpty ( dt . Rows [ 0 ] [ "t" ] . ToString ( ) ) == true ? DateTime . Now : Convert . ToDateTime ( dt . Rows [ 0 ] [ "t" ] . ToString ( ) );
            else
                return DateTime . Now;
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum ,string num)
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT IBB001,IBB003,IBB004,IBB041,IBB961,CONVERT(FLOAT,IBB006) IBB006,CONVERT(FLOAT,IBB980) IBB980,CONVERT(FLOAT,IBB981) IBB981 FROM DCSIBB A INNER JOIN QIXDEL B ON A.IBB001=B.DEL017 WHERE DEL016='{0}' AND DEL017='{1}'" ,num ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum ,string num )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL001,DEL002,DEL003,DEL004,DEL005,DEL006,CONVERT(FLOAT,DEL007) DEL007,CONVERT(FLOAT,DEL008) DEL008,CONVERT(FLOAT,DEL009) DEL009,DEL010,CONVERT(FLOAT,DEL007*DEL008+DEL009) U0 FROM QIXDEL  WHERE DEL017='{0}' AND DEL016='{1}'" ,oddNum ,num );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 审核  反审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Examine ( SiftingDataEntity . MainEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE QIXDEL SET DEL014=@DEL014 WHERE DEL011=@DEL011" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@DEL011",SqlDbType.NVarChar,50),
                new SqlParameter("@DEL014",SqlDbType.Bit)
            };
            parameter [ 0 ] . Value = model . DEL011;
            parameter [ 1 ] . Value = model . DEL014;

            int rows = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            return rows > 0 ? true : false;
        }

    }
}
