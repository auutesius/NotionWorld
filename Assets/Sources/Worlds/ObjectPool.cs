using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NotionWorld.Worlds
{
    public static class ObjectPool
    {
        /// <summary>
        /// 对象池
        /// </summary>
        private static readonly Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

        /// <summary>
        /// 预设体
        /// </summary>
        private static readonly Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

        static ObjectPool()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        /// <summary>
        /// 从对象池中获取对象 
        /// </summary>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static GameObject GetObject(string objName, string path)
        {
            //结果对象
            GameObject result = null;
            //判断是否有该名字的对象池
            if (pool.ContainsKey(objName))
            {
                //对象池里有对象
                if (pool[objName].Count > 0)
                {
                    //获取结果
                    result = pool[objName].Dequeue();
                    if (result == null)
                    {
                        return null;
                    }
                    //激活对象
                    result.SetActive(true);
                    //返回结果
                    return result;
                }
            }
            //如果没有该名字的对象池或者该名字对象池没有对象

            GameObject prefab = null;
            //如果已经加载过该预设体
            if (prefabs.ContainsKey(objName))
            {
                prefab = prefabs[objName];
            }
            else     //如果没有加载过该预设体
            {
                //加载预设体
                prefab = Resources.Load<GameObject>("Prefabs/" + path + "/" + objName);
                //更新字典
                prefabs.Add(objName, prefab);
            }

            //生成
            result = UnityEngine.Object.Instantiate(prefab);
            //改名（去除 Clone）
            result.name = objName;
            result.SetActive(true);
            //返回
            return result;
        }

        /// <summary>
        /// 回收对象到对象池
        /// </summary>
        /// <param name="objName"></param>
        public static void RecycleObject(GameObject obj)
        {
            if (obj.activeSelf == true)
            {
                //设置为非激活
                obj.SetActive(false);
                //判断是否有该对象的对象池
                if (pool.ContainsKey(obj.name))
                {
                    //放置到该对象池
                    pool[obj.name].Enqueue(obj);
                }
                else
                {
                    //创建该类型的池子，并将对象放入
                    pool.Add(obj.name, new Queue<GameObject>());
                    pool[obj.name].Enqueue(obj);
                }
            }
        }

        public static void Clear()
        {
            pool.Clear();
            prefabs.Clear();
        }

        private static void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            Clear();
        }

    }
}