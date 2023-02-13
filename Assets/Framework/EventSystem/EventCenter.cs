using UnityEngine;
using TFramework.EventSystem;
using System;
using System.Collections.Generic;

namespace TFramework.ApplicationLevel
{
    public class TEventType
    {
        ////封面上的两个按钮事件
        //public const string InitGame = "InitGame";//进入游戏按钮事件
        //public const string ReturnMenu = "ReturnMenu";//返回目录按钮事件

        ////进入游戏后的所有按钮事件
        //public const string StartScene = "StartScene";//开场按钮事件
        //public const string GameScene = "GameScene";//游戏场景按钮事件
        //public const string GameOver = "GameOver";//游戏结束按钮事件
        //public const string RealGameOver = "RealGameOver";//结束面板上结束游戏按钮事件
        //public const string RestartGame = "RestartGame";//恢复游戏按钮事件

        //游戏中固定的事件
        public const string AddScore = "AddScroe";//计分板得分事件
        public const string TweenOver = "TweenOver";//各个组件动画取消事件

        /*****************************************每个游戏单独的事件*******************************************************/

        /*****************************************每个游戏单独的事件*******************************************************/
    }

    public class EventCenter : MonoBehaviour, IEventCenter
    {
        public void IClearAllListener()
        {
            throw new System.NotImplementedException();
        }

        private void InitView()
        {
            
        }

        public void IInitManagers()
        {

        }

        public void IRegisterAllListener()
        {
            TEventSystem.Instance.EventManager.addEventListener(TEventType.AddScore, AddScore);
            TEventSystem.Instance.EventManager.addEventListener(TEventType.TweenOver, TweenOver);
        }
        /// <summary>
        /// 暂停Tweener动画
        /// </summary>
        /// <param name="evt"></param>
        private void TweenOver(TEvent evt)
        {
            List<BaseItem> baseItems = (List<BaseItem>)evt.eventParams[0];
            for (int i = 0; i < baseItems.Count; i++)
            {
                baseItems[i].PauseTweener();
            }
        }

        /***************************逻辑实现方法************************************/

        /// <summary>
        /// 设置计分板事件
        /// </summary>
        /// <param name="evt"></param>
        private void AddScore(TEvent evt)
        {
            UIManager.GetInstance().AddScore();
        }
        void Awake()
        {
            InitView();
            IInitManagers();
            IRegisterAllListener();
            TEventSystem.Instance.EventManager.dispatchEvent(new TEvent(TEventType.TweenOver, new List<BaseItem>()), this);
        }
    }
}
