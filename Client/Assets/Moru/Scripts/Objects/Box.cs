using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class Box : MonoBehaviour
    {
        /// <summary>
        /// 현재 박스가 담고 있는 상품목록들입니다.
        /// </summary>
        [ShowInInspector]
        public Dictionary<MoruDefine.Product, int> cur_PuttingItem = new Dictionary<MoruDefine.Product, int>();

        public int DiscorrectCount = 0;


        /// <summary>
        /// 박스가 포장된 상태인지를 체크하는 불린
        /// </summary>
        public bool isPacking = false;

        private void Start()
        {
            if (cur_PuttingItem.Count != (int)MoruDefine.Product.MAX)
            {
                for (int i = 0; i < (int)MoruDefine.Product.MAX; i++)
                {
                    cur_PuttingItem.Add((MoruDefine.Product)i, 0);
                }
            }
        }

        /// <summary>
        /// 박스가 삭제될 때 게임매니저가 관리하는 박스수 잔량이 업데이트됩니다.
        /// </summary>
        private void OnDestroy()
        {
            //여기서 발생하는 에러는 무시해도 됩니다.
            GameManager.Instance.OnRemoveBox();
        }

        /// <summary>
        /// 상품을 박스에 담습니다. 매개변수만큼 담을 수 있습니다. (기본값 = 1)
        /// </summary>
        /// <param name="product">상품명</param>
        /// <param name="howMay">얼마나 담을 것이냐</param>
        public void Put_Product(MoruDefine.Product product, int howMay = 1)
        {
            int count = cur_PuttingItem[product];
            count += howMay;
            cur_PuttingItem[product] = count;

            Debug.Log($"박스에 {product}를 {howMay}만큼 담았습니다. 현재 카운드 : {count}" +
                $"\n 테스트용 랜덤값이 삽입되는 것을 주의하세요.");
        }

        public void CompletePacking(int DiscorrectCount)
        {
            isPacking = true;
            this.DiscorrectCount = DiscorrectCount;
        }
    }
}