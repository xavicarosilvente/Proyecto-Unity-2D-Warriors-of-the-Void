using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCharacter : MonoBehaviour
{

    [System.Serializable]
    public class CharacterProperties
    {
        public string Name;
        public int ID;
        public int Life;
        public int NumLifes;
        public Vector2 Attack;
        public Vector2 Defense;
        public int Velocity;
        public GameObject Prefab;

        public CharacterProperties(string _Name, int _ID, int _Life, int _NumLifes, Vector2 _Attack, Vector2 _Defense,
            int _Velocity, GameObject _Prefab)
        {
            Name = _Name;
            ID = _ID;
            Life = _Life;
            NumLifes = _NumLifes;
            Attack = _Attack;
            Defense = _Defense;
            Velocity = _Velocity;
            Prefab = _Prefab;
        }
    }

    public CharacterProperties GetCharacter(int ID)
    {
        CharacterProperties TempChar = Characters[ID];
        CharacterProperties CloneChar = new CharacterProperties
        (
            TempChar.Name, TempChar.ID, TempChar.Life, TempChar.NumLifes, TempChar.Attack, TempChar.Defense, TempChar.Velocity,
            TempChar.Prefab
        );
        return CloneChar;
    }

    public List<CharacterProperties> Characters;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }    
}
