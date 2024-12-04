using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menupausa2 : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private bool juegoPausado = false;

    private void Start()
    {
        // Asegurarse de que el menú de pausa está cerrado al iniciar el juego
        menuPausa.SetActive(false);
        botonPausa.SetActive(true);

        // Asegurarse de que el cursor está en el estado correcto al iniciar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }

        // Tecla R para reiniciar el nivel
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reiniciar();
        }

        // Tecla M para salir del juego
        if (Input.GetKeyDown(KeyCode.M))
        {
            Cerrar();
        }
    }

    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);

        // Hacer el cursor visible y desbloqueado mientras está en pausa
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

        // Ocultar el cursor al reanudar
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Asegurarte de desbloquear el cursor al reiniciar si es necesario
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Cerrar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);

        // Hacer el cursor visible y desbloqueado al cargar la nueva escena
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
