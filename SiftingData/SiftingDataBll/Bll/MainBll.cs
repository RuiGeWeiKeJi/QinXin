using System . Collections . Generic;
using System . Data;

namespace SiftingDataBll . Bll
{
    public class MainBll
    {
        private readonly Dao.MainDao dal=null;

        public MainBll ( )
        {
            dal = new Dao . MainDao ( );
        }

        /// <summary>
        /// 获取单头数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableQuery ( )
        {
            return dal . getTableQuery ( );
        }

        /// <summary>
        /// 获取主件品号信息
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public DataTable getTableQueryPin ( string orderNum )
        {
            return dal . getTableQueryPin ( orderNum );
        }

        /// <summary>
        /// 获取品号
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePNum ( )
        {
            return dal . getTablePNum ( );
        }

        /// <summary>
        /// 获取配方单号
        /// </summary>
        /// <returns></returns>
        public DataTable getTableOrder ( )
        {
            return dal . getTableOrder ( );
        }

        /// <summary>
        /// 获取子表
        /// </summary>
        /// <param name="gdNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere)
        {
            return dal . getTableView ( strWhere );
        }

        /// <summary>
        /// 根据子件品号获取批号
        /// </summary>
        /// <param name="pNum"></param>
        /// <returns></returns>
        public DataTable getTablePiHao ( string pNum )
        {
            return dal . getTablePiHao ( pNum );
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <returns></returns>
        public bool Save ( DataTable table ,string oderNum ,string remark )
        {
            return dal . Save ( table ,oderNum ,remark );
        }

        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Update ( DataTable table ,string oderNum ,List<int> idxList ,string remark   )
        {
            return dal . Update ( table ,oderNum ,idxList ,remark  );
        }

        /// <summary>
        /// 工单是否审核
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool ExistsSH ( string orderNum )
        {
            return dal . ExistsSH ( orderNum );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool Delete ( string orderNum,string num )
        {
            return dal . Delete ( orderNum ,num );
        }

        /// <summary>
        /// 插入工单单身数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SaveOrder ( string oddNum )
        {
            return dal . SaveOrder ( oddNum );
        }

        /// <summary>
        /// 生成领料单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SavePick ( string oddNum ,string num)
        {
            return dal . SavePick ( oddNum ,num );
        }

        /// <summary>
        /// 生成领料单  不覆盖上次的
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int SavePickOne ( string oddNum ,string num ,string piNum,string numOfOrder)
        {
            return dal . SavePickOne ( oddNum ,num ,piNum,numOfOrder );
        }


        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum ,string num)
        {
            return dal . getTablePrintOne ( oddNum ,num );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum ,string num )
        {
            return dal . getTablePrintTwo ( oddNum ,num );
        }

        /// <summary>
        /// 审核  反审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Examine ( SiftingDataEntity . MainEntity model )
        {
            return dal . Examine ( model );
        }

    }
}
