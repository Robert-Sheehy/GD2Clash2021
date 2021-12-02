using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject character_prefab_template;
    public GameObject dragon_prefab_template;
    public GameObject Cannon_Temp;
    public GameObject townhall_template;
    public GameObject witch_template;



    List<CharacterScript> allCharacters;
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

    internal void spawn_th_at(Vector3 point)
    {
        GameObject new_buildingGO = Instantiate(townhall_template,
                point, Quaternion.identity);
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
            allCharacters.Add(new_characterScript);
        }

    }

    internal void spawn_drag_at(Vector3 point)
    {
        GameObject new_characterGO = Instantiate(dragon_prefab_template,
                      point, Quaternion.identity);

        DragonScript newDragonScript = new_characterGO.GetComponent<DragonScript>();

        if (newDragonScript)
        {
            newDragonScript.ImtheMan(this);
            allCharacters.Add(newDragonScript);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allCharacters = new List<CharacterScript>();
        allBuildings = new List<Building>();
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
                allCharacters.Add(newCharacterScript);
            }

        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject new_characterGO = Instantiate(witch_template,
                            new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)), Quaternion.identity);

            CharacterScript newCharacterScript = new_characterGO.GetComponent<CharacterScript>();

            if (newCharacterScript)
            {
                newCharacterScript.ImtheMan(this);
                allCharacters.Add(newCharacterScript);
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject new_characterGO = Instantiate(dragon_prefab_template,
                            new Vector3(Random.Range(-10f, 10f), 10, Random.Range(-10f, 10f)), Quaternion.identity);

            DragonScript newDragonScript = new_characterGO.GetComponent<DragonScript>();

            if (newDragonScript)
            {
                newDragonScript.ImtheMan(this);
                allCharacters.Add(newDragonScript);
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
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject new_CannonGO = Instantiate(Cannon_Temp,
                new Vector3(Random.Range(-60f, 60f), 0, Random.Range(-60f, 60f)), Quaternion.identity);
            Building new_CannonScript = new_CannonGO.GetComponent<CannonScript>();

            if(new_CannonScript)
            {
                new_CannonScript.ImtheMan(this);
                allBuildings.Add(new_CannonScript);
            }

        }
        if(Input.GetKeyDown(KeyCode.X))
        {
          
        }
    }


    internal void Im_Dying(Unit unit)
    {
        if (unit is Building)
        {
            Building destroyed_Building = unit as Building;

            foreach (CharacterScript character in allCharacters)
                character.is_destroyed(destroyed_Building);
        }

        if (unit is CharacterScript)
        {
            CharacterScript killed_character = unit as CharacterScript;

            foreach (Building building in allBuildings)
                building.is_destroyed(killed_character);
        }

    }
    internal void Im_Dead(Unit unit)
    {
        if (unit is Building)
        {
            Building destroyed_Building = unit as Building;
            allBuildings.Remove(destroyed_Building);
        }

        if (unit is CharacterScript)
        {
            CharacterScript killed_character = unit as CharacterScript;
            allCharacters.Remove(killed_character);
        }

    }

    internal Unit whats_my_target(Unit unit)
    {


        if (unit is CharacterScript)
        {
            float distance = 100000f;
            Building nearest = null;
            CharacterScript characterScript = unit as CharacterScript;
            foreach (Building next_building in allBuildings)
            {   if ((next_building.current_state != Building.Unit_States.Dying) && (next_building.current_state != Building.Unit_States.Dead))
                if ((Vector3.Distance(characterScript.transform.position, next_building.transform.position) < distance))
                {
                    distance = Vector3.Distance(characterScript.transform.position, next_building.transform.position);
                    nearest = next_building;
                }
            }

            return nearest;
        }

        else
        {

            float distance = 100000f;
            CharacterScript nearest = null;


            Building buildingScript = unit as Building;
            foreach (CharacterScript next_character in allCharacters)
            {
                if (Vector3.Distance(buildingScript.transform.position, next_character.transform.position) < distance)
                {
                    distance = Vector3.Distance(buildingScript.transform.position, next_character.transform.position);
                    nearest = next_character;
                }
            }

            return nearest;

        }

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
            foreach (CharacterScript character in allCharacters)
            {

                if (Vector3.Distance(position, character.transform.position) < radius)
                {

                    character.takeDamage(damage);

                }

            }
        }

        allCharacters = tidy(allCharacters);
        allBuildings = tidy(allBuildings);
    }

    private List<Building> tidy(List<Building> list_of_building)
    {
        List<Building> new_list = new List<Building>();
        foreach (Building b in list_of_building)
            if (b) new_list.Add(b);

        return new_list;

    }

    private List<CharacterScript> tidy(List<CharacterScript> list_of_characters)
    {
        List<CharacterScript> new_list = new List<CharacterScript>();
        foreach (CharacterScript b in list_of_characters)
            if (b) new_list.Add(b);

        return new_list;
    }

}
