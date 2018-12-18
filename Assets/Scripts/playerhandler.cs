using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerhandler : MonoBehaviour {

    public GameObject ref_snakep; //Referencia a pedazo de serpiente
    enum direcciones { arriba, abajo, izquierda, derecha };
    direcciones direccion = direcciones.derecha; //Por defecto va hacia la derecha inicialmente
    public Vector2 casillero_actual;
    bool comio_manzana = false;
    public List<GameObject> snakep; //Partes de la serpiente

    gamehandler ref_gh;

	// Use this for initialization
	void Start () {
        ref_gh = GameObject.Find("GameManager").GetComponent<gamehandler>();
        Invoke("_move", 0.5f - ref_gh.puntos/10.0f);
	}

    void _move()
    {


        for (int i = snakep.Count-1; i >= 1; i--) //Actualizo las partes de la serpiente
        {
            snakep[i].GetComponent<snake_p>().casillero_actual = snakep[i-1].GetComponent<snake_p>().casillero_actual;
            snakep[i].transform.position = snakep[i - 1].transform.position;
        }

        //Actualizo la parte de la serpiente proxima a la cabeza
        if (snakep.Count > 0)
        {
            snakep[0].GetComponent<snake_p>().casillero_actual = casillero_actual;
            snakep[0].transform.position = transform.position;
        }

        if (comio_manzana)
        {
            GameObject newSnakeP = Instantiate(ref_snakep);
            newSnakeP.transform.localScale = transform.localScale;
            newSnakeP.GetComponent<snake_p>().casillero_actual = casillero_actual;
            newSnakeP.transform.position = transform.position;
            snakep.Add(newSnakeP); //Se agrega una parte a la lista de partes
            comio_manzana = false;
        }

        switch(direccion)
        {
            case direcciones.arriba:
                transform.position += new Vector3(0, transform.GetComponent<SpriteRenderer>().bounds.size.y, 0);
                casillero_actual.y -= 1;
                break;

            case direcciones.abajo:
                transform.position += new Vector3(0, -transform.GetComponent<SpriteRenderer>().bounds.size.y, 0);
                casillero_actual.y += 1;
                break;

            case direcciones.derecha:
                transform.position += new Vector3(transform.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                casillero_actual.x += 1;
                break;

            case direcciones.izquierda:
                transform.position += new Vector3(-transform.GetComponent<SpriteRenderer>().bounds.size.x, 0, 0);
                casillero_actual.x -= 1;
                break;
        }

        check_collision(); //Chequeo colisiones

        //Chequeo salida del tablero
        if((casillero_actual.x > ref_gh.casilleros.x-1) || (casillero_actual.y > ref_gh.casilleros.y-1) || casillero_actual.x < 0 || casillero_actual.y < 0)
        {
            ref_gh._gameover();
        }

        if(!ref_gh.gameover)
            Invoke("_move", 0.5f - (ref_gh.puntos /10 /20));
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(direccion != direcciones.izquierda)
                direccion = direcciones.derecha;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direccion != direcciones.derecha)
                direccion = direcciones.izquierda;
        }
            
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (direccion != direcciones.abajo)
                direccion = direcciones.arriba;
        }
            
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direccion != direcciones.arriba)
                direccion = direcciones.abajo;
        }
            

	}

    void check_collision()
    {
        if(GameObject.Find("Manzana").GetComponent<manzana>().casillero_actual == casillero_actual)
        {
            comio_manzana = true;
            Destroy(GameObject.Find("Manzana"));
            ref_gh.spawn_apple();
            ref_gh.add_puntos(10);
        }


        for (int i = 0; i < snakep.Count; i++) //Actualizo las partes de la serpiente
        {
            if(snakep[i].GetComponent<snake_p>().casillero_actual == casillero_actual)
            {
                ref_gh._gameover();
                return;
            }
            
        }
    }


}
