# Práctica 1 - Desarrollo de Videojuegos

**Autor:** Álvaro Morazo Mosquera
**Curso:** 4º Ingeniería Informática
**Profesora:** Elisa Todd

## 1. Descripción del Proyecto
Esta práctica consiste en la creación de un entorno 3D en Unity donde se han implementado mecánicas de movimiento, interacción y gestión de plataformas. Se ha utilizado el **Universal Render Pipeline (URP)** para la gestión visual.

## 2. Planteamiento y Resolución de la Práctica
A continuación, se detalla cómo se han resuelto los puntos principales y los **apartados opcionales** implementados:

### Jugador y Control (Puntos Extra e Implementaciones Propias)
* **Locomoción Avanzada:** Además del movimiento básico, se ha implementado un sistema de **Sprint** y **Doble Salto**, superando los requisitos base de la práctica.
* **Sistema de Cámara y Orientación:** Se ha configurado el personaje para que siempre dé la espalda o el frente a la cámara (evitando el perfil) y mire dinámicamente hacia la dirección de movimiento.
* **Animaciones:** Se ha configurado un *Animator Controller* completo que gestiona estados de `Idle`, `Walk`, `Jump` y `Fall`.
* **Partículas:** Se han integrado sistemas de partículas con texturas personalizadas para mejorar el *feedback* visual (Orbs Effects).

### Plataformas (Tipos A y B)
* **Plataformas Móviles (`MovingPlatformClase.cs`):** Resuelto mediante movimiento cíclico entre puntos. Se ha completado el **punto opcional** de emparentado: el jugador se convierte en "hijo" de la plataforma al pisarla para mantener la inercia del movimiento.
* **Plataformas que Caen (`FallingPlatform.cs`):** Implementadas con un sistema de detección superior, cuenta atrás y reinicio automático a la posición inicial tras la caída.

### Entorno y Gestión de Assets
* **Corrección de Assets:** Se realizó una corrección manual del *colormap* de los bloques de construcción para asegurar que la hierba se visualice verde y no morada.
* **Contenido Adicional:** Se han introducido **NPCs con animaciones** y diversos objetos decorativos ajenos a los proporcionados, creando sus respectivos *prefabs* para poblar la escena.

## 3. Estructura del Repositorio
El proyecto sigue la estructura organizada solicitada en el enunciado:
* **Assets/Scripts:** Lógica de programación (`PlayerController.cs`, `MovingPlatformClase.cs`, `FallingPlatform.cs`).
* **Assets/Prefabs:** Objetos preconfigurados, incluyendo los nuevos assets y NPCs.
* **Assets/Models:** Modelos 3D y texturas originales y corregidas.
* **Assets/Scenes:** Escena principal del proyecto (`SampleScene`).


# Práctica 2 - Continuación de Plataformas en Unity

## 1. Descripción del Proyecto
Este proyecto amplía la base de la práctica anterior para construir un sistema de juego completo y funcional. Se ha implementado un bucle de juego que incluye recolección de monedas, sistema de vidas, respawn mediante puntos de control y transiciones entre múltiples niveles y escenas de menú. 

El proyecto cumple estrictamente con el requisito de utilizar **Unity 6000.3.8f1** y una estructura organizada de carpetas (Scripts, Prefabs, Scenes, UI, Audio).

## 2. Planteamiento y Resolución de la Práctica

### Sistema de Juego y GameManager
* **Patrón Singleton:** Se ha implementado un `GameManager` bajo el patrón Singleton para centralizar el estado global del juego (monedas y vidas) de forma persistente entre escenas.
* **Gestión de Escenas:** Implementación de un bucle de juego funcional: `Menu` -> `Nivel 1` -> `Nivel 2` -> `End`.

### Interfaz de Usuario (UI) y Feedback
* **UI Dinámica:** Un script `UpdateUI` se suscribe a eventos (`Actions`) del `GameManager` para actualizar el contador de monedas y el sistema visual de vidas.
* **Sistema de Vidas Visual:** Se han implementado **imágenes de corazones** que desaparecen dinámicamente conforme el jugador recibe daño.
* **Audio:** Integración de efectos sonoros para amenizar la experiencia de juego.

### Mecánicas de Supervivencia y Respawn
* **Zonas de Daño y Enemigos:** Creación de objetos dañinos (pinchos con animación, sierra) que restan vidas al contacto.
* **Animación de Muerte:** Se ha configurado un *Trigger* `Die` en el Animator del jugador. Al recibir daño, el movimiento se bloquea temporalmente para reproducir la animación antes de reaparecer.
* **Checkpoints y Optimización (Opcional):** Se ha implementado el sistema de **puntos de control**. Al tocar un checkpoint, se actualiza la posición de reaparición del jugador. Para mejorar el rendimiento, los checkpoints se desactivan tras su primer uso.

## 3. Apartados Opcionales Implementados
Siguiendo las sugerencias del enunciado y aportaciones propias, se han incluido:
1.  **Múltiples Niveles:** El juego cuenta con dos niveles diferenciados con progresión de dificultad.
2.  **Sistema de Checkpoints Funcional:** El jugador no vuelve al inicio del nivel si ha activado un punto de control previo.
3.  **UI con Imágenes:** Uso de sprites para representar la salud en lugar de simple texto.
4.  **Optimización de Triggers:** Deshabilitación de objetos coleccionables y checkpoints tras su activación para liberar carga de procesamiento.
5.  **Bucle de Animación Completo:** Inclusión de estados de muerte y reaparición sincronizados con la lógica de juego.

## 4. Estructura del Repositorio
* **Assets/Scripts:** Lógica central (`GameManager.cs`, `PlayerController.cs`, `UpdateUI.cs`, `Goal.cs...`).
* **Assets/Prefabs:** Prefabs organizados de monedas, enemigos, plataformas y checkpoints.
* **Assets/Scenes:** * `Menu`: Menú de inicio con botón Play.
    * `Nivel1` / `Nivel2`: Escenas de juego.
    * `End`: Pantalla final con opciones de reintento.
* **Assets/Audio:** Clips de sonido.