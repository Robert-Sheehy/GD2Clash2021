using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject character_prefab_template;
    public GameObject townhall_template;


    List<CharacterScript> allUnits;
    List<Building> allBuildings;

    internal void AddTHAt(Vector3 position)
    {
        GameObject new_buildingGO = Instantiate(townhall_template,
                position, Quaternion.identity);
        Building new_buildingScript = new_buildingGO.GetComponent<Building>();

        if (new_buildingScript)
        {
            new_buildingScript.ImtheMan(this);
            allBuildings.Add(new_buildingScript);
        }
    }

    internal void AddChar(Vector3 position)
    {
        GameObject new_charGO = Instantiate(character_prefab_template, position, Quaternion.identity);
        CharacterScript new_characterScript = new_charGO.GetComponent<CharacterScript>();

        if(new_characterScript)
        {
            new_characterScript.ImtheMan(this);
            allUnits.Add(new_characterScript);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        allUnits = new List<CharacterScript>();
        allBuildings = new List<Building>();
    }

    internal Building whats_my_target(Witch witch)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
           GameObject new_characterGO = Instantiate(character_prefab_template,
                           new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);

            CharacterScript newCharacterScript = new_characterGO.GetComponent<CharacterScript>();

            if (newCharacterScript)
            {
                newCharacterScript.ImtheMan(this);
                allUnits.Add(newCharacterScript);
            }

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject new_buildingGO = Instantiate(townhall_template,
                           new Vector3(Random.Range(-200f, 200f), 0, Random.Range(-200f, 200f)), Quaternion.identity);
            Building new_buildingScript = new_buildingGO.GetComponent<Building>();

            if (new_buildingScript)
            {
                new_buildingScript.ImtheMan(this);
                 allBuildings.Add(new_buildingScript);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.X))
        {
          
        }
    }

    internal void Im_Dead(Building building)
    {
        allBuildings.Remove(building);
        foreach (CharacterScript character in allUnits)
            character.is_destroyed(building);

    }

    internal Building whats_my_target(CharacterScript characterScript)
    { float distance = 100000f;
        Building nearest = null;
     foreach (Building next_building in allBuildings)
        {
            if (Vector3.Distance(characterScript.transform.position,next_building.transform.position) < distance)
            {
                distance = Vector3.Distance(characterScript.transform.position, next_building.transform.position);
                nearest = next_building;
            }
        }

        return   nearest;
    }

    public void AOE_Attack(Vector3 position, float radius, int damage, bool attackBuilding)
    {

        if (attackBuilding == true)
        {

            foreach (Building next_building in allBuildings)
            {
                 if (Vector3.Distance(position, next_building.transform.position) < radius)
                 {

                 next_building.takeDamage(damage);

                 }
            
            }

        }
        else
        { 
            foreach (CharacterScript character in allUnits)
            {

             if (Vector3.Distance(position, character.transform.position) < radius)
                {

                character.takeDamage(damage);

                }

            }
        }
    }

}
