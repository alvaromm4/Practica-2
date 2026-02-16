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
