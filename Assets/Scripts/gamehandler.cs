using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamehandler : MonoBehaviour {

    public GameObject ref_bckg; //Referencia al Background a instanciar
    public GameObject ref_snake; //Referencia a la serpiente a instanciar
    public GameObject ref_apple;
    public GameObject ref_txtpuntos;
    public GameObject ref_txtgo;
    public Vector2Int casilleros;
    public int puntos = 0;
    public bool gameover = false;

	// Use this for initialization
	void Start () {
        GameObject newSnk = Instantiate(ref_snake); //Instancio el background teniendo en cuenta la referencia dada
        
        GameObject newBckg = Instantiate(ref_bckg); //Instancio el background teniendo en cuenta la referencia dada
        newBckg.name = "Background";

        newSnk.transform.localScale = new Vector3(1.0f * newBckg.transform.localScale.x / casilleros.x, 1.0f * newBckg.transform.localScale.y / casilleros.y, 0); //Esto esta de puta madre perfecto
        newSnk.transform.position += new Vector3(newSnk.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f * casilleros.x, newSnk.transform.GetComponent<SpriteRenderer>().bounds.size.y * casilleros.y / -2.0f, 0);
        newSnk.transform.position += new Vector3(newSnk.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f, newSnk.transform.GetComponent<SpriteRenderer>().bounds.size.y/ -2.0f, 0); //Ajusto al centro
        newSnk.GetComponent<playerhandler>().casillero_actual = new Vector2(casilleros.x / 2, casilleros.y / 2); //Le doy la info a la manzana de su casillero actual

        spawn_apple();
    }
	
    public void spawn_apple()
    {
        GameObject newApp = Instantiate(ref_apple); //Instancio el background teniendo en cuenta la referencia dada
        GameObject newBckg = GameObject.Find("Background");
        Vector3 casillero_manzana = new Vector3(Random.Range(0, casilleros.x), Random.Range(0, casilleros.y), 0); //Busco al azar un casillero para la manzana teniendo en cuenta los maximos del tablero
        newApp.transform.localScale = new Vector3(1.0f * newBckg.transform.localScale.x / casilleros.x, 1.0f * newBckg.transform.localScale.y / casilleros.y, 0); //Esto esta de puta madre perfecto
        newApp.transform.position += new Vector3(newApp.transform.GetComponent<SpriteRenderer>().bounds.size.x * casillero_manzana.x, newApp.transform.GetComponent<SpriteRenderer>().bounds.size.y * -casillero_manzana.y, 0);
        newApp.transform.position += new Vector3(newApp.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f, newApp.transform.GetComponent<SpriteRenderer>().bounds.size.y / -2.0f, 0);
        newApp.GetComponent<manzana>().casillero_actual = new Vector2(casillero_manzana.x, casillero_manzana.y); //Le doy la info a la manzana de su casillero actual
        newApp.name = "Manzana";
    }


	// Update is called once per frame
	void Update () {
		
	}

    public void add_puntos(int add)
    {
        puntos += add;
        ref_txtpuntos.GetComponent<Text>().text = "PUNTOS: " + puntos.ToString();
    }

    public void _gameover()
    {
        gameover = true;
        ref_txtgo.SetActive(true); //Habilito texto gameover
        Invoke("restart_level", 2.0f);
    }


    public void restart_level()
    {
        SceneManager.LoadScene(0);
    }
}
