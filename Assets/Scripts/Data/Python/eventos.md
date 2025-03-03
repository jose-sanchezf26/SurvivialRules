# Eventos de Survival Rules

| User  | Game ID  | Session ID | Event Type | Time  | Data  | Descripción  |
|-------|-------------|------------------------------------|------------|---------------------|-------|---------------|
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-log_in  | 17/02/2025 17:01:21 | {}    | Inicia sesión |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-log_out | 17/02/2025 17:18:20 | {}    | Cierra sesión |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-start_game | 17/02/2025 17:18:20 | {} | Empieza una partida |
| Jose  | Survival Rules | 2d2bb501-78f8-4dd9-99a1-ad80160f2792      | sr-end_game | 03/03/2025 12:30:58         | {"timeSurvived": 86, "way": "death"}           | Termina una partida |
| Jose | Survival Rules | a8110894-4a66-4309-bdc4-9e650d1b9bb8 |sr-save_sbr| 25/02/2025 09:03:36 | {"fileName": "ArchivoSBR.BE2", "sbr": "<Block>..</Block>"} | Guarda el sbr en un archivo |
| Jose | Survival Rules | a8110894-4a66-4309-bdc4-9e650d1b9bb8 |sr-load_sbr| 25/02/2025 09:03:36 | {"fileName": "ArchivoSBR.BE2", "sbr": "<Block>..</Block>"} | Carga el sbr en un archivo |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_fb | 17/02/2025 17:24:21 | {} | Abre la base de hechos |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-close_fb | 17/02/2025 17:01:43 | {} | Cierra la base de hechos |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_kb | 17/02/2025 17:24:20 | {} | Abre la base de conocimientos |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_s/l | 17/02/2025 17:24:25 | {} | Abre el menú de guardar/cargar |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-close_s/l | 17/02/2025 17:24:50 | {} | Cierra el menú de guardar/cargar |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_rules_log | 17/02/2025 17:24:19 | {} | Abre el log de las reglas |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-open_inventory | 17/02/2025 17:24:16 | {} | Abre el inventario |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-start_tutorial | 17/02/2025 17:28:09 | {} | Empieza el tutorial |
| Jose  | Survival Rules  | b3c33340-53bc-4d65-b237-9e61d3c31ec7  | sr-tutorial_next   | 20/02/2025 12:07:48   | {"windowName": "Creación de reglas", "stepsNumber": 4}     | Avanza a la siguiente ventana del tutorial |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-end_tutorial | 17/02/2025 17:28:22 | {} | Termina el tutorial |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-create_block | 17/02/2025 17:29:18 | {"blockType": "WhenPlayClicked", "blockId": "84"} | Crea un bloque |
| Jose  | Survival Rules  | e9308fad-8dd8-4d10-b252-d47d4c627dc3  | sr-delete_block   | 22/02/2025 19:25:23   | {"blockType": "GetProperty", "blockId": "88", "positionX": -67.530014, "positionY": 25.0, "parentBlockType": "Sum", "parentBlockId": "84", "parentRelation": "input", "positionInParent": "2", "sbr": "<Block>...</Block>#"}  | Elimina un bloque |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-drop_env | 17/02/2025 17:29:19 | {"blockType": "WhenPlayClicked", "blockId": "84", "positionX": 209.94165, "positionY": -275.427, "sbr": "<Block>...</Block>"} | Deja un bloque en el entorno |
| Jose  | Survival Rules  | 2c8852fd-9b43-4410-8734-48554fecf868  | sr-drop_stack     | 22/02/2025 18:57:53   | {"parentBlockType": "WhenPlayClicked", "parentBlockId": "85", "positionInParent": "3", "blockType": "SetTarget", "blockId": "84", "positionX": 0.0, "positionY": -130.0, "sbr": "<Block>...</Block>"} | Deja un bloque en el body de otro |
| Jose  | Survival Rules  | 2c8852fd-9b43-4410-8734-48554fecf868  | sr-drop_input     | 22/02/2025 18:59:12   | {"parentBlockType": "RuleWithID", "parentBlockId": "88", "positionInParent": "2", "blockType": "GetProperty", "blockId": "90", "positionX": -9.00999451, "positionY": 30.0, "sbr": "<Block>...</Block>"} | Deja un bloque de entrada en el entorno con múltiples bloques hijos  |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-select_f_env | 17/02/2025 17:39:45 | {"blockType": "Explore", "blockId": "85", "positionX": -82.62988, "positionY": 44.09955} | Selecciona un bloque que estaba en el entorno |
| Jose  | Survival Rules  | 2c8852fd-9b43-4410-8734-48554fecf868  | sr-select_f_stack | 22/02/2025 18:50:22   | {"parentBlockType": "WhenPlayClicked", "parentBlockId": "85", "positionInParent": "2", "blockType": "SetTarget", "blockId": "84", "positionX": -51.26229, "positionY": 51.8326874} | Selecciona un bloque del body de otro|
| Jose  | Survival Rules  | 2c8852fd-9b43-4410-8734-48554fecf868  | sr-select_f_input | 22/02/2025 18:53:49   | {"parentBlockType": "RuleWithID", "parentBlockId": "88", "positionInParent": "2", "blockType": "GetProperty", "blockId": "90", "positionX": -83.5153046, "positionY": 33.135498} | Selecciona un bloque del input de otro |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-pause | 17/02/2025 17:49:53 | {"sbr": "..."} | Pausa el juego y la ejecución de las reglas |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-play | 17/02/2025 17:49:54 | {"sbr": "..."} | Reanuda el juego y ejecuta las reglas de ese instante |
| Jose  | Survival Rules | 13072935-16e3-4483-8f38-73d4e3ecf693 | sr-mod_speed | 17/02/2025 17:43:00 | {"oldSpeed": "x1", "newSpeed": "x2"} | Modifica la velocidad del juego a una dada |
| Jose  | Survival Rules | ce9a07a5-3a7c-42a8-9460-9941f286cd2e  | sr-open_context_menu  | 20/02/2025 10:44:39  | {"blockType": "WhenPlayClicked", "blockId": "84", "positionX": 215.9338, "positionY": -293.42804} | Abre el menú contextual de un bloque |
| Jose  | Survival Rules | e5d489e7-22c0-4113-bad6-db84ec77e1fd  | sr-close_context_menu  | 20/02/2025 11:16:53  | {"blockType": "WhenPlayClicked", "blockId": "84", "positionX": 349.798462, "positionY": -398.607544} | Cierra el menú contextual de un bloque |
| Jose  | Survival Rules | 12b31741-88e5-41f5-b1c1-1e47b597dcfe  | sr-duplicate_block  | 20/02/2025 11:28:49  | {"blockType": "WhenPlayClicked", "blockDuplicatedId": "84", "newBlockId": "86", "positionX": 236.916565, "positionY": -293.481445, "sbr": "..."} | Duplica un bloque en el entorno |
| Jose  | Survival Rules  | 2c8852fd-9b43-4410-8734-48554fecf868  | sr-select_input  | 22/02/2025 18:48:29   | {"blockType": "SetTarget", "blockId": 84, "inputPosition": 1, "textValue": "hola"}  | Selecciona el input de un bloque   |
| Jose  | Survival Rules  | 2c8852fd-9b43-4410-8734-48554fecf868  | sr-deselect_input | 22/02/2025 18:48:31   | {"blockType": "SetTarget", "blockId": 84, "inputPosition": 1, "textValue": "adios"} | Deselecciona un input en un bloque |
| Jose  | Survival Rules  | 0a814f77-04ed-4a9f-ae72-2fe561f2ac9a  | sr-change_drop_down  | 22/02/2025 20:06:26   | {"blockType": "DetectObject", "blockId": 84, "dropDownPosition": 1, "oldValue": "is", "newValue": "is not"}  | Cambia una opción de un desplegable en "DetectObject"  |
| Jose  | Survival Rules | a742cdde-2d96-4db5-95bf-563028af53aa | sr-modify_difficulty | 03/03/2025 12:02:44 | {"oldDifficulty": 1, "newDifficulty": 2} | Se modifica la dificultad del juego |


Cambios en la semana del 17/02/2025:
- Interfaz:
    - Cambio de color en el inventario
    - Botón añadido al morir para guardar el sbr
    - Botón de salir más accesible
    - Iconos en los atributos del jugador
    - Indicador de cuando el juego está pausado

- Eventos:
    - Añadidos:
        - Abre el menú contextual de un bloque (sr-open_context_menu)
        - Duplica un bloque (sr-duplicate_block)
        - Cierra el menú contextual (sr-close_context_menu)
        - Avanza de ventana dentro del tutorial (sr_tutorial_next)
        - Selecciona un input de un bloque (sr-select_input)
        - Deseleccona un input de un bloque (sr-deselect_input)
        - Cambia el valor en un dropdown (sr-change_drop_down)
        - Guarda el sbr en un archivo (sr-save_sbr)
        - Carga el sbr de un archivo (sr-load_sbr)
        - Abre el menú de guardar/cargar (sr-open_s/l)
        - Cierra el menú de guardar/cargar (sr-close_s/l)
    - Modificaciones:
        - En el event sr-delete_block se muestra el sbr, el bloque padre (si tenía), si se encontraba en el body o en el input del bloque padre y su posición dentro de él
        - El evento sr-continue ahora se llama sr-play
        - En los eventos sr-select_f_input, sr-select_stack, sr-drop_input y sr-drop_stack muestran la posición en la que se selecciona/suelta el bloque dentro del padre
        - El evento sr-mod_speed sustituye a los anteriores, mostrando ahora la velocidad anterior y la actual

Cambios en la semana del 24/02/2025:
- Interfaz:
    - Cambiado el icono de pausa.
    - Cambiado el color del tooltip según el bloque.
    - Añadido un timer en la pantalla y un indicador de la dificultad.
- Eventos
  - Añadidos:
    - Evento para cuando se modifica la dificultad del juego
  - Modificaciones:
    - En el evento end_game, se ha añadido el tiempo que ha sobrevivido el jugador, además de la forma de terminar la partida (le da a exit o muere el personaje).

- Añadida la funcionalidad para aumentar la dificultad del juego.
