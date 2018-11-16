using System;
using System . Data;
using DevExpress . XtraEditors;
using System . Linq;
using System . Collections . Generic;
using System . Windows . Forms;

namespace SiftingData
{
    public partial class FormMain :FormChild
    {
        SiftingDataBll.Bll.MainBll _bll=null;
        SiftingDataEntity . MainEntity model = null;
        DataTable tableView, tablePNum,tablePrintOne,tablePrintTwo,tableQuery,tableOrder;
        string state=string.Empty,resultCal = string . Empty,strWhere=string.Empty;
        bool result=false; decimal sum = 0M;
        List<int> idxList=new List<int>();

        public FormMain ( )
        {
            InitializeComponent ( );

            _bll = new SiftingDataBll . Bll . MainBll ( );
            model = new SiftingDataEntity . MainEntity ( );

            barTool . LinksPersistInfo . RemoveAt ( toolCanecl . Id );
            barTool . LinksPersistInfo . RemoveAt ( toolSave . Id );
            barTool . LinksPersistInfo . RemoveAt ( toolDelete . Id );
            barTool . LinksPersistInfo . RemoveAt ( toolEdit . Id );
            barTool . LinksPersistInfo . RemoveAt ( toolAdd . Id );

            getQueryForColumn ( );
            Utility . GridViewMoHuSelect . SetFilter ( gridLookUpEdit1View );
            Utility . GridViewMoHuSelect . SetFilter ( sunOfNumA );
            Utility . GridViewMoHuSelect . SetFilter ( gridView2 );
            Utility . GridViewMoHuSelect . SetFilter ( gridView3 );
            Utility . GridViewMoHuSelect . SetFilter ( gridView4 );
            controlUnEnable ( );
            layoutControlItem13 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;

        }

        #region Main
        protected override int Query ( )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) && string . IsNullOrEmpty ( txtDEL015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择订单号或配方单号" );
                return 0;
            }
            if ( !string . IsNullOrEmpty ( txtRAA001 . Text ) && string . IsNullOrEmpty ( txtDEA002 . Text ) )
            {
                XtraMessageBox . Show ( "请选择主件品号" );
                return 0;
            }

            strWhere = "1=1";

            if ( !string . IsNullOrEmpty ( txtDEL015 . Text ) )
                strWhere += " AND DEL015='" + txtDEL015 . Text + "'";
            else
            {
                if ( !string . IsNullOrEmpty ( txtRAA001 . Text ) )
                    strWhere += " AND DEL017='" + txtRAA001 . Text + "'";
                if ( !string . IsNullOrEmpty ( txtDEA002 . Text ) )
                    strWhere += " AND DEL016='" + txtDEA002 . Text + "'";
                if ( !string . IsNullOrEmpty ( txtDEL018 . Text ) )
                    strWhere += " AND DEL018='" + txtDEL018 . Text + "'";
            }

            tableView = _bll . getTableView ( strWhere );
            gridControl1 . DataSource = tableView;
            if ( tableView == null || tableView . Rows . Count < 1 )
                return 0;

            txtRAA001 . EditValue = txtRAA001 . Text = tableView . Rows [ 0 ] [ "DEL017" ] . ToString ( );
            //txtRAA015 . EditValue = txtRAA015 . Text = tableView . Rows [ 0 ] [ "DEL011" ] . ToString ( );
            txtDEL018 . EditValue = txtDEL018 . Text = string . Empty;
           txtDEL018 . EditValue = txtDEL018 . Text = tableView . Rows [ 0 ] [ "DEL018" ] . ToString ( );
            //txtDEA002 . Text = tableView . Rows [ 0 ] [ "DEL016" ] . ToString ( );
            txtDEL013 . Text = tableView . Rows [ 0 ] [ "DEL013" ] . ToString ( );
            string txtExamin = tableView . Rows [ 0 ] [ "DEL014" ] . ToString ( );
            layoutControlItem13 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            if ( string . IsNullOrEmpty ( txtExamin ) )
                btn . Text = "审核";
            else
            {
                if ( txtExamin . Equals ( "True" ) )
                {
                    btn . Text = "反审核";
                    layoutControlItem13 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                }
                else
                {
                    btn . Text = "审核";
                    layoutControlItem13 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                }
            }
            QueryTool ( );
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlEnable ( );
            gridControl1 . DataSource = null;

            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择订单号" );
                return 0;
            }
            if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择主件品号" );
                return 0;
            }

            //tableView = _bll . getTableView ( txtRAA001 . Text ,txtRAA015 . Text );
            gridControl1 . DataSource = tableView;
            if ( tableView != null && tableView . Rows . Count > 0 )
            {
                editTool ( );
                state = "edit";
                txtDEL013 . Text = tableView . Rows [ 0 ] [ "DEL013" ] . ToString ( );
                return 0;
            }
            else
                txtDEL013 . Text = string . Empty;

            state = "add";
            addTool ( );

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            editTool ( );

            state = "edit";

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "订单号不可为空" );
                return 0;
            }
            if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
            {
                XtraMessageBox . Show ( "主件品号不可为空" );
                return 0;
            }
            if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
            {
                result = _bll . Delete ( txtRAA001 . Text ,txtRAA015 . Text );
                if ( result )
                {
                    XtraMessageBox . Show ( "删除成功" );
                    controlClear ( );
                    controlUnEnable ( );
                    deleteTool ( );
                }
                else
                    XtraMessageBox . Show ( "删除失败" );
            }

            return base . Delete ( );
        }
        protected override int Print ( )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择订单号" );
                return 0;
            }
            if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择主件品号" );
                return 0;
            }
            getPrintTable ( );

            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"加工通知单.frx" );

            return base . Print ( );
        }
        protected override int Export ( )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择订单号" );
                return 0;
            }
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择主件品号" );
                return 0;
            }
            getPrintTable ( );

            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"加工通知单.frx" );

            return base . Export ( );
        }
        protected override int Save ( )
        {
            if ( checkTable ( ) == false )
                return 0;

            if ( state . Equals ( "add" ) )
                result = _bll . Save ( tableView ,txtRAA001 . Text ,txtDEL013 . Text );
            else if ( state . Equals ( "edit" ) )
                result = _bll . Update ( tableView ,txtRAA001 . Text ,idxList ,txtDEL013 . Text );

            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                saveTool ( );
                controlUnEnable ( );
                idxList . Clear ( );
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
            else
                XtraMessageBox . Show ( "保存失败" );

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            if ( state . Equals ( "add" ) )
                controlClear ( );
            controlUnEnable ( );
            cancelTool ( state );
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Cancel ( );
        }
        protected override int QueryAll ( )
        {
            tableQuery = _bll . getTableQuery ( );
            txtRAA001 . Properties . DataSource = tableQuery;
            txtRAA001 . Properties . DisplayMember = "IBB001";
            txtRAA001 . Properties . ValueMember = "IBB001";

            tablePNum = _bll . getTablePNum ( );
            sonOfNum . DataSource = tablePNum;
            sonOfNum . DisplayMember = "DEA001";
            sonOfNum . ValueMember = "DEA001";

            txtDEL015 . Properties . DataSource = _bll . getTableOrder ( );
            txtDEL015 . Properties . DisplayMember = "DEL015";
            txtDEL015 . Properties . ValueMember = "DEL015";

            return base . QueryAll ( );
        }
        private void btn_Click ( object sender ,EventArgs e )
        {
            SiftingDataEntity . MainEntity model = new SiftingDataEntity . MainEntity ( );
            state = btn . Text;
            if ( state . Equals ( "审核" ) )
                model . DEL014 = true;
            else
                model . DEL014 = false;

            model . DEL011 = txtRAA001 . Text;
            result = _bll . Examine ( model );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                //if ( state . Equals ( "审核" ) )
                //    btn . Text = "反审核";
                //else if ( state . Equals ( "反审核" ) )
                //    btn . Text = "审核";
                examine ( state );
            }
            else
                XtraMessageBox . Show ( state + "失败" );
        }
        #endregion

        #region Event
        private void txtRAA001_EditValueChanged ( object sender ,EventArgs e )
        {
            //DataRow row = gridLookUpEdit1View . GetFocusedDataRow ( );
            //if ( row == null )
            //{
                if ( txtRAA001 . EditValue == null )
                    return;
                if ( tableQuery . Select ( "IBB001='" + txtRAA001 . EditValue + "'" ) . Length < 1 )
                    return;
                DataRow row = tableQuery . Select ( "IBB001='" + txtRAA001 . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
            //}
            if ( txtRAA001 . EditValue == null )
                return;
            if ( string . IsNullOrEmpty ( txtRAA001 . EditValue . ToString ( ) ) )
                return;
            if ( !string . IsNullOrEmpty ( row [ "IBB001" ] . ToString ( ) ) && row [ "IBB001" ] . ToString ( ) . Equals ( txtRAA001 . EditValue . ToString ( ) ) )
            {
                txtRAA003 . Text = row [ "IBA003" ] . ToString ( );
                txtDFA002 . Text = row [ "DFA002" ] . ToString ( );
                
                tableOrder = _bll . getTableQueryPin ( txtRAA001 . EditValue . ToString ( ) );
                txtDEL018 . Properties . DataSource = tableOrder;
                txtDEL018 . Properties . DisplayMember = "IBB002";
                txtDEL018 . Properties . ValueMember = "IBB002";
            }
        }
        private void txtRAA015_EditValueChanged ( object sender ,EventArgs e )
        {
            //DataRow row = gridView3 . GetFocusedDataRow ( );
            //if ( row == null )
            //{
                if ( txtRAA015 . EditValue == null || txtRAA015 . EditValue . ToString ( ) == string . Empty )
                    return;
                if ( tableOrder == null || tableOrder . Rows . Count < 1 )
                    return;
                if ( tableOrder . Select ( "IBB003='" + txtRAA015 . EditValue + "'" ) . Length < 1 )
                    return;
                DataRow row = tableOrder . Select ( "IBB003='" + txtRAA015 . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
            //}
            if ( txtRAA015 . EditValue == null )
                return;
            txtDEA002 . Text = row [ "IBB004" ] . ToString ( );
            txtDEA057 . Text = row [ "IBB041" ] . ToString ( );
            txtIBB961 . Text = row [ "IBB961" ] . ToString ( );
            txtRAA018 . Text = row [ "IBB006" ] . ToString ( );
            txtIBB981 . Text = row [ "IBB981" ] . ToString ( );
            txtIBB980 . Text = row [ "IBB980" ] . ToString ( );
            txtIBB005 . Text = row [ "IBB005" ] . ToString ( );
        }
        private void txtDEL018_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtDEL018 . EditValue == null || txtDEL018 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( tableOrder == null || tableOrder . Rows . Count < 1 )
                return;
            if ( tableOrder . Select ( "IBB002='" + txtDEL018 . EditValue + "'" ) . Length < 1 )
                return;
            DataRow row = tableOrder . Select ( "IBB002='" + txtDEL018 . EditValue + "'" ) [ 0 ];
            if ( row == null )
                return;
            if ( txtDEL018 . EditValue == null )
                return;
            txtDEA002 . Text = row [ "IBB004" ] . ToString ( );
            txtDEA057 . Text = row [ "IBB041" ] . ToString ( );
            txtIBB961 . Text = row [ "IBB961" ] . ToString ( );
            txtRAA018 . Text = row [ "IBB006" ] . ToString ( );
            txtIBB981 . Text = row [ "IBB981" ] . ToString ( );
            txtIBB980 . Text = row [ "IBB980" ] . ToString ( );
            txtIBB005 . Text = row [ "IBB005" ] . ToString ( );
            txtRAA015 . Text = row [ "IBB003" ] . ToString ( );
        }
        private void sonOfNum_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView1 . ActiveEditor;
            if ( edit == null )
                return;
            if ( gridView1 . FocusedColumn . FieldName . Equals ( "DEL001" ) )
            {
                if ( edit . EditValue == null )
                    return;
                if ( string . IsNullOrEmpty ( edit . EditValue . ToString ( ) ) )
                    return;
                DataRow row = tablePNum . Select ( "DEA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( !row [ "DEA001" ] . ToString ( ) . Equals ( edit . EditValue ) )
                    return;
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL002" ] ,row [ "DEA002" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL003" ] ,row [ "DEA057" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL004" ] ,row [ "DEA003" ] . ToString ( ) );
            }
        }
        private void btnPNum_Click ( object sender ,EventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            FormMainQuery form = new FormMainQuery ( row [ "DEL001" ] . ToString ( ) ,"批号查询" );
            form . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                Dictionary<int ,string> dicSpe = form . getSpe;
                Dictionary<int ,string> dicWare = form . getWare;
                if ( dicSpe != null && dicSpe . Count > 0 )
                {
                    for ( int i = 0 ; i < dicSpe . Count ; i++ )
                    {
                        if ( i == 0 )
                        {
                            gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL005" ] ,dicSpe [ i ] );
                            gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL006" ] ,dicWare [ i ] );
                        }
                        else
                        {
                            DataRow rowAdd = tableView . NewRow ( );
                            rowAdd [ "DEL001" ] = row [ "DEL001" ] . ToString ( );
                            rowAdd [ "DEL002" ] = row [ "DEL002" ] . ToString ( );
                            rowAdd [ "DEL003" ] = row [ "DEL003" ] . ToString ( );
                            rowAdd [ "DEL004" ] = row [ "DEL004" ] . ToString ( );
                            rowAdd [ "DEL005" ] = dicSpe [ i ];
                            rowAdd [ "DEL006" ] = dicWare [ i ];
                            tableView . Rows . Add ( rowAdd );
                        }
                    }
                }
            }
        }
        //生成工单
        private void btnOne_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "工单单号不可为空" );
                return ;
            }

            int resu = _bll . SaveOrder ( txtRAA001 . Text );
            if ( resu == -1 )
                XtraMessageBox . Show ( "标准系统没有此工单" );
            else if ( resu == -2 )
                XtraMessageBox . Show ( "单身无数据" );
            else if ( resu == 1 )
                XtraMessageBox . Show ( "成功写入工单" );
            else
                XtraMessageBox . Show ( "写入工单失败" );

        }
        //生成领料单
        private void btnTwo_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择订单号" );
                return;
            }
            if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择住件品号" );
                return;
            }
            int resu =/* _bll . SavePick ( txtRAA001 . Text ,txtRAA015 . Text );*/
                 _bll . SavePickOne ( txtRAA001 . Text ,txtDEA002 . Text ,txtIBB961 . Text ,txtDEL018 . Text );
            if ( resu == -1 )
                XtraMessageBox . Show ( "单身无数据" );
            else if ( resu == -2 )
                XtraMessageBox . Show ( "标准系统已经存在领料单" );
            else if ( resu == 1 )
                XtraMessageBox . Show ( "成功写入领料单" );
            else
                XtraMessageBox . Show ( "写入领料单失败" );
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            if ( e . KeyChar == 13 )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    DataRow row = gridView1 . GetFocusedDataRow ( );
                    if ( row == null )
                        return;
                    int idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( idx > 0 )
                    {
                        if ( !idxList . Contains ( idx ) )
                            idxList . Add ( idx );
                    }
                    tableView . Rows . Remove ( row );
                    gridControl1 . RefreshDataSource ( );
                    row = null;
                    sum = 0;
                    resultCal = string . Empty;
                    caclu ( sum ,resultCal ,row );
                }
            }
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "DEL007" || e . Column . FieldName == "DEL008" || e . Column . FieldName == "DEL009" )
            {
                
                gridView1 . CloseEditor ( );
                gridView1 . UpdateCurrentRow ( );
                DataRow row = null;
                sum = 0;
                resultCal = string . Empty;
                sum = caclu ( sum ,resultCal ,row );

                row = gridView1 . GetDataRow ( e . RowHandle );
                if ( row != null )
                {
                    model . DEL007 = string . IsNullOrEmpty ( row [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL007" ] . ToString ( ) );
                    model . DEL008 = string . IsNullOrEmpty ( row [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL008" ] . ToString ( ) );
                    model . DEL009 = string . IsNullOrEmpty ( row [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL009" ] . ToString ( ) );
                    resultCal = sum == 0 ? 0 . ToString ( ) : Math . Round ( Convert . ToDecimal ( model . DEL007 * model . DEL008 + model . DEL009 ) / sum * 100 ,2 ) + "%";
                    gridView1 . SetRowCellValue ( e . RowHandle ,gridView1 . Columns [ "DEL010" ] ,resultCal );
                }
            }
        }
        private void txtRAA001_TextChanged ( object sender ,EventArgs e )
        {
            if ( txtRAA001 . Text == string . Empty )
                txtRAA003 . Text = txtRAA018 . Text = txtIBB981 . Text = txtIBB980 . Text = txtIBB961 . Text = txtIBB005 . Text = string . Empty;
        }
        private void txtRAA015_TextChanged ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
                txtDEA002 . Text = txtDEA057 . Text = string . Empty;
        }
        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtDEA002 . Text = txtDEA057 . Text = txtIBB961 . Text = txtIBB980 . Text = txtIBB981 . Text = txtRAA001 . Text = txtRAA003 . Text = txtRAA015 . Text = txtRAA018 . Text = txtDEL013 . Text = txtDEL015 . Text = string . Empty;
        }
        #endregion

        #region Method
        void getQueryForColumn ( )
        {
            tableQuery = _bll . getTableQuery ( );
            txtRAA001 . Properties . DataSource = tableQuery;
            txtRAA001 . Properties . DisplayMember = "IBB001";
            txtRAA001 . Properties . ValueMember = "IBB001";

            tablePNum = _bll . getTablePNum ( );
            sonOfNum . DataSource = tablePNum;
            sonOfNum . DisplayMember = "DEA001";
            sonOfNum . ValueMember = "DEA001";

            txtDEL015 . Properties . DataSource = _bll . getTableOrder ( );
            txtDEL015 . Properties . DisplayMember = "DEL015";
            txtDEL015 . Properties . ValueMember = "DEL015";

        }
        void controlEnable ( )
        {
            gridView1 . OptionsBehavior . Editable = true;
            txtDEL013 . Enabled = true;
        }
        void controlUnEnable ( )
        {
            gridView1 . OptionsBehavior . Editable = false;
            txtDEL013 . Enabled = false;
        }
        void controlClear ( )
        {
            txtDEA002 . Text = txtDEA057 . Text = txtIBB961 . Text = txtIBB980 . Text = txtIBB981 . Text = txtRAA001 . Text = txtRAA003 . Text =  txtRAA015 . Text = txtRAA018 . Text = txtDEL013 . Text = string . Empty;
            gridControl1 . DataSource = null;
        }
        bool checkTable ( )
        {
            result = true;
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请选择单身数据" );
                return false;
            }

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            t1 = p . Field<string> ( "DEL001" ) ,
                            t2 = p . Field<string> ( "DEL005" ) ,
                            t3 = p . Field<string> ( "DEL006" )
                        } into g
                        select new
                        {
                            del001 = g . Key . t1 ,
                            del002 = g . Key . t2 ,
                            del003 = g . Key . t3 ,
                            count = g . Count ( )
                        };

            if ( query == null )
                return true;

            foreach ( var co in query )
            {
                if ( co != null && !string . IsNullOrEmpty ( co . ToString ( ) ) )
                {
                    if ( co . count > 1 )
                    {
                        XtraMessageBox . Show ( "子件品号:" + co . del001 + "\n\r批号:" + co . del002 + "\n\r仓库:" + co . del003 + "\n\r重复出现,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            var select = tableView . AsEnumerable ( )
                . Where ( p => p . Field<string> ( "DEL001" ) == null || p . Field<string> ( "DEL001" ) == "" )
                . Select ( p => new
                {
                    del001 = p . Field<string> ( "DEL001" )
                }
                );
            if ( select != null )
            {
                foreach ( var p in select )
                {
                    if ( p == null || string . IsNullOrEmpty ( p . del001 ) )
                    {
                        XtraMessageBox . Show ( "子件品号不可为空" );
                        return false;
                    }
                }
            }

            select = null;
            select = tableView . AsEnumerable ( )
                 . Where ( p => p . Field<string> ( "DEL005" ) == null || p . Field<string> ( "DEL005" ) == "" )
                 . Select ( p => new
                 {
                     del001 = p . Field<string> ( "DEL005" )
                 }
                 );
            if ( select != null )
            {
                foreach ( var p in select )
                {
                    if ( p == null || string . IsNullOrEmpty ( p . del001 ) )
                    {
                        XtraMessageBox . Show ( "批号不可为空" );
                        return false;
                    }
                }
            }

            //select = null;
            //select = tableView . AsEnumerable ( )
            //     . Where ( p => p . Field<object> ( "DEL007" ) == null || p . Field<int> ( "DEL007" ) == 0 )
            //     . Select ( p => new
            //     {
            //         del001 = p . Field<object> ( "DEL007" ) == null ? 0 . ToString ( ) : p . Field<int> ( "DEL007" ) . ToString ( )
            //     }
            //     );
            //if ( select != null )
            //{
            //    foreach ( var p in select )
            //    {
            //        if ( p == null || string . IsNullOrEmpty ( p . del001 ) || Convert . ToInt32 ( p . del001 ) <= 0 )
            //        {
            //            XtraMessageBox . Show ( "件数必须大于0" );
            //            return false;
            //        }
            //    }
            //}

            //select = null;
            //select = tableView . AsEnumerable ( )
            //     . Where ( p => p . Field<object> ( "DEL008" ) == null || p . Field<decimal> ( "DEL008" ) == 0M )
            //     . Select ( p => new
            //     {
            //         del001 = p . Field<object> ( "DEL008" ) == null ? 0 . ToString ( ) : p . Field<decimal> ( "DEL008" ) . ToString ( )
            //     }
            //     );
            //if ( select != null )
            //{
            //    foreach ( var p in select )
            //    {
            //        if ( p == null || string . IsNullOrEmpty ( p . del001 ) || Convert . ToDecimal ( p . del001 ) <= 0M )
            //        {
            //            XtraMessageBox . Show ( "件重必须大于0" );
            //            return false;
            //        }
            //    }
            //}

            //select = null;
            //select = tableView . AsEnumerable ( )
            //     . Where ( p => p . Field<object> ( "DEL010" ) == null || p . Field<decimal> ( "DEL010" ) == 0M )
            //     . Select ( p => new
            //     {
            //         del001 = p . Field<object> ( "DEL010" ) == null ? 0 . ToString ( ) : p . Field<decimal> ( "DEL010" ) . ToString ( )
            //     }
            //     );
            //if ( select != null )
            //{
            //    foreach ( var p in select )
            //    {
            //        if ( p == null || string . IsNullOrEmpty ( p . del001 ) || Convert . ToDecimal ( p . del001 ) <= 0M )
            //        {
            //            XtraMessageBox . Show ( "拼配比例必须大于0" );
            //            return false;
            //        }
            //    }
            //}

            return result;
        }
        void getPrintTable ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtRAA001 . Text ,txtRAA015 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtRAA001 . Text ,txtRAA015 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void examine ( string state )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            if ( state . Equals ( "审核" ) )
            {
                btn . Text = "反审核";
                //Concell1 . Show ( );           
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                layoutControlItem13 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
            }
            else
            {
                btn . Text = "审核";
                //Concell1 . Hide ( );
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                layoutControlItem13 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            }
        }
        decimal caclu ( decimal sum ,string resultCal ,DataRow row )
        {
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                model . DEL007 = string . IsNullOrEmpty ( row [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( row [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( row [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL009" ] . ToString ( ) );
                sum += Convert . ToDecimal ( model . DEL007 * model . DEL008 + model . DEL009 );
            }
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                model . DEL007 = string . IsNullOrEmpty ( row [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( row [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( row [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL009" ] . ToString ( ) );
                resultCal = sum == 0 ? 0 . ToString ( ) : Math . Round ( Convert . ToDecimal ( model . DEL007 * model . DEL008 + model . DEL009 ) / sum * 100 ,2 ) + "%";
                gridView1 . SetRowCellValue ( i ,gridView1 . Columns [ "DEL010" ] ,resultCal );
            }

            return sum;
        }
        #endregion

    }
}