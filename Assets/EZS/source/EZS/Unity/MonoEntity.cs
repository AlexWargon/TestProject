using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Wargon.ezs.Unity
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(ExecutionOrder.MONO_ENTITY)]
    public class MonoEntity : MonoBehaviour
    {
        public Entity Entity;
        [SerializeReference] public List<object> Components = new();
        public bool runTime;
        public bool destroyObject;
        public bool destroyComponent;
        public int id;
        private bool converted;
        private GameObject GO;
        private World world;
        public int ComponentsCount => runTime ? Entity.GetEntityData().ComponentsCount : Components.Count;

        private void Start()
        {
            ConvertToEntity();
        }

        private void OnDestroy()
        {
            if (!destroyObject)
                if (world != null)
                    if (world.Alive)
                        if (!Entity.IsNULL())
                            Entity.DestroyLate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual ref Entity ConvertToEntity()
        {
            if (converted) return ref Entity;
            GO = gameObject;
            Entity = MonoConverter.GetWorld().CreateEntity();
            world = Entity.World;
#if UNITY_EDITOR
            gameObject.name = $"{gameObject.name} ID:{Entity.id.ToString()}";
#endif
            id = Entity.id;
            MonoConverter.Execute(ref Entity, Components, this);
            converted = true;
            if (destroyComponent) Destroy(this);
            if (destroyObject) Destroy(gameObject);
            runTime = true;
            return ref Entity;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get<T>() where T : struct
        {
            return ref Entity.Get<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove<T>() where T : struct
        {
            Entity.Remove<T>();
        }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public void SetActive(bool state)
        // {
        //     if (state)
        //         Enable();
        //     else
        //         Disable();
        //     GO.SetActive(state);
        // }

        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private void Disable()
        // {
        //     if (!converted) return;
        //     if (!world.Alive) return;
        //     if (Entity.IsNULL()) return;
        //     Entity.Add<Inactive>();
        // }
        //
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // private void Enable()
        // {
        //     if (!converted) return;
        //     if (Entity.Has<Inactive>())
        //         Entity.Remove<Inactive>();
        // }

        public void DestroyWithoutEntity()
        {
            destroyObject = true;
            Destroy(GO);
        }
#if UNITY_EDITOR
        // private void OnEnable()
        // {
        //     Enable();
        // }
        //
        // private void OnDisable()
        // {
        //     Disable();
        // }
#endif
    }

    [EcsComponent]
    public struct View
    {
        public MonoEntity Value;
    }

    public static class MonoEntityExtension
    {
        public static string ToJson(this MonoEntity monoEntity)
        {
            var components = monoEntity.Components;
            var monoEntityJson = $"{monoEntity.name} :";
            monoEntityJson += Environment.NewLine;
            for (var index = 0; index < components.Count; index++)
            {
                var component = components[index];
                var type = component.GetType();
                var componentJson = string.Empty;
                var classOrStruct = type.IsValueType ? "struct" : "class";
                componentJson += $"ComponentIndex:{index} [type:{type.Name},{classOrStruct}:";
                componentJson += Environment.NewLine;
                componentJson += "      Fields:";
                for (var i = 0; i < type.GetFields().Length; i++)
                {
                    var fieldInfo = type.GetFields()[i];
                    componentJson += Environment.NewLine;
                    componentJson += "      ";
                    var name = fieldInfo.Name;
                    var fieldType = fieldInfo.FieldType;
                    var fieldValue = fieldInfo.GetValue(component);
                    componentJson += $"fieldIndex:{i} ;";
                    componentJson += $"Name:{name} ;";
                    componentJson += $"Type:{fieldType} ;";
                    componentJson += "Value:";
                    if (fieldValue != null)
                        componentJson += fieldValue.ToString();
                    else
                        componentJson += "NULL";
                    componentJson += ";";
                }

                componentJson += "];";
                monoEntityJson += componentJson;
                monoEntityJson += Environment.NewLine;
            }

            File.WriteAllText(Application.dataPath + $"/{monoEntity.name}.json", monoEntityJson);
            return monoEntityJson;
        }

        public static void FromJson(this MonoEntity monoEntity, TextAsset file)
        {
            var json = file.text;
            var fLines = Regex.Split(json, "\n|\r|\r\n");
            for (var i = 0; i < fLines.Length; i++)
            {
                Debug.Log(i);
                var line = fLines[i];
                if (line.Contains($"ComponentIndex:{i}"))
                {
                    var pFrom = line.IndexOf("type:") + "type:".Length;
                    var pTo = line.LastIndexOf(",");

                    var result = line.Substring(pFrom, pTo - pFrom);
                    Debug.Log(result);
                }
            }
        }
    }
}