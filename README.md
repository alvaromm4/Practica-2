# Práctica 3 - Desarrollo de FPS e Inteligencia Artificial

**Autor:** Álvaro Morazo Mosquera
**Curso:** 4º Ingeniería Informática
**Profesora:** Elisa Todd

## 1. Descripción del Proyecto
Este proyecto consiste en la creación de un sistema de disparos en primera persona (FPS) funcional. Se han implementado mecánicas de movimiento, un sistema de vida compartido, enemigos con IA mediante NavMesh y una interfaz de usuario dinámica para el seguimiento de la salud.

## 2. Planteamiento y Resolución de la Práctica

### Sistema de Control (FPS)
* **Locomoción:** Se ha implementado un controlador basado en `Rigidbody` que gestiona el movimiento del jugador y las colisiones con el entorno.
* **Efecto de Zoom (ADS):** Al mantener pulsado el clic derecho del ratón, se activa un sistema de zoom mediante la reducción dinámica del **Field of View (FOV)** de la cámara, mejorando la precisión visual del jugador.
* **Disparo:** Mecánica de combate basada en `Raycast` que detecta impactos en tiempo real y aplica daño a los objetos que contienen el componente de salud.

### Inteligencia Artificial (NavMesh)
* **Comportamientos:** Los enemigos utilizan el sistema de navegación `NavMeshAgent` para perseguir al jugador de forma inteligente, configurando rangos de visión y distancias de parada óptimas.
* **Sistema de Daño IA:** Para garantizar un combate fluido y evitar conflictos de colisión, se ha implementado un ataque basado en `Raycast` frontal que permite a los enemigos infligir daño al jugador por cadencia cuando se encuentran a corta distancia.
* **Variaciones de IA:** El proyecto incluye diferentes tipos de enemigos, desde persecutores directos hasta unidades con rutas de patrullaje cíclico.

### Interfaz y Vida (UI)
* **Sistema de Salud:** Gestión de vida mediante el script `Health.cs`. Se ha configurado un bucle de juego donde la muerte del jugador reinicia la escena y la de los enemigos destruye el objeto.
* **UI Dinámica:** Implementación de una barra de salud visual (*Slider*) que utiliza `Color.Lerp` para transicionar de verde a rojo según el daño recibido.

## 3. Estructura del Repositorio
* **Assets/Scripts:** Lógica de control, IA y gestión de interfaz (`PlayerController2.cs`, `Enemy.cs`, `Health.cs`, `UIManager.cs`).
* **Assets/Prefabs:** Objetos preconfigurados de enemigos, jugador y elementos de UI.
* **Assets/Scenes:** Escena principal de combate con el NavMesh horneado.