# Eventos de survival Rules

| User  | Game ID  | Event Type | Time  | Data  | Descripción  |
|-------|-------------|------------|-------|-------|---------------|
|Jose||sr-log_in|17/02/2025 17:01:21|{}| Inicia sesión|
|Jose||sr-log_out|17/02/2025 17:18:20|{}| Cierra sesión|
|Jose|Jose - 17/02/2025 17:18:07|sr-start_game|17/02/2025 17:18:20|{}| Empieza una partida|
|Jose|Jose - 17/02/2025 17:18:07|sr-end_game|17/02/2025 17:20:20|{}| Termina una partida|
| Jose  | Jose - 17/02/2025 17:24:12  | sr-open_fb  | 17/02/2025 17:24:21  | {}   | Abre la base de hechos|
| Jose  | Jose - 17/02/2025 17:01:22  | sr-close_fb       | 17/02/2025 17:01:43   | {}   | Cierra la base de hechos
| Jose  | Jose - 17/02/2025 17:24:12  | sr-open_kb  | 17/02/2025 17:24:20  | {}   | Abre la base de conocimientos|
| Jose  | Jose - 17/02/2025 17:24:12  | sr-open_rules_log  | 17/02/2025 17:24:19  | {}   | Abre el log de las reglas
| Jose  | Jose - 17/02/2025 17:24:12  | sr-open_inventory  | 17/02/2025 17:24:16  | {}   | Abre el inventario
| Jose  | Jose - 17/02/2025 17:24:12  | sr-start_tutorial  | 17/02/2025 17:28:09  | {}   | Empieza el tutorial
| Jose  | Jose - 17/02/2025 17:24:12  | sr-end_tutorial    | 17/02/2025 17:28:22  | {}   | Termina el tutorial
| Jose  | Jose - 17/02/2025 17:24:12  | sr-create_block   | 17/02/2025 17:29:18  | {"blockType": "WhenPlayClicked", "blockId": "84"} | Crea un bloque
| Jose  | Jose - 17/02/2025 17:24:12  | sr-delete_block  | 17/02/2025 17:37:01  | {"blockType": "WhenPlayClicked", "blockId": "84"} | Elimina un bloque
| Jose  | Jose - 17/02/2025 17:24:12  | sr-drop_env       | 17/02/2025 17:29:19  | {"blockType": "WhenPlayClicked", "blockId": "84", "positionX": 209.94165, "positionY": -275.427, "sbr": "<Block>...</Block>"} | Deja un bloque en el entorno
| Jose  | Jose - 17/02/2025 17:24:12  | sr-select_f_env   | 17/02/2025 17:39:45   | {"blockType": "Explore", "blockId": "85", "positionX": -82.62988, "positionY": 44.09955} | Selecciona un bloque que estaba en el entorno
| Jose  | Jose - 17/02/2025 17:24:12  | sr-drop_stack     | 17/02/2025 17:31:20  | {"parentBlockType": "WhenPlayClicked", "parentBlockId": "84", "blockType": "Explore", "blockId": "85", "positionX": 0.0, "positionY": 10.0, "sbr": "<Block>...</Block>"} | Suelta un bloque dentro de otro
| Jose  | Jose - 17/02/2025 17:24:12  | sr-select_f_stack | 17/02/2025 17:31:21  | {"parentBlockType": "WhenPlayClicked", "parentBlockId": "84", "blockType": "Explore", "blockId": "85", "positionX": -61.7938652, "positionY": 48.7296562} | Selecciona un bloque que estaba en otro
| Jose  | Jose - 17/02/2025 17:24:12  | sr-drop_input     | 17/02/2025 17:34:12  | {"parentBlockType": "SetTarget", "parentBlockId": "86", "blockType": "Sum", "blockId": "87", "positionX": -46.2100067, "positionY": 30.0, "sbr": "<Block>...</Block>"} | Suelta un bloque en la sección de input de otro
| Jose  | Jose - 17/02/2025 17:24:12  | sr-select_f_input | 17/02/2025 17:34:12  | {"parentBlockType": "SetTarget", "parentBlockId": "86", "blockType": "Sum", "blockId": "87", "positionX": -107.567993, "positionY": 24.5014248} | Selecciona un bloque que estaba la sección de input de otro
| Jose  | Jose - 17/02/2025 17:49:47  | sr-pause          | 17/02/2025 17:49:53   | {"sbr": "..."} | Pausa el juego y la ejecución de las reglas
| Jose  | Jose - 17/02/2025 17:49:47  | sr-continue       | 17/02/2025 17:49:54   | {"sbr": "..."} | Reanuda el juego y ejecuta las reglas de ese instante
| Jose  | Jose - 17/02/2025 17:24:12  | sr-mod_speed_x1   | 17/02/2025 17:43:00   | {}   | Modifica la velocidad del juego a x1
| Jose  | Jose - 17/02/2025 17:24:12  | sr-mod_speed_x2   | 17/02/2025 17:43:01   | {}   | Modifica la velocidad del juego a x2
| Jose  | Jose - 17/02/2025 17:24:12  | sr-mod_speed_x3   | 17/02/2025 17:43:02   | {}   | Modifica la velocidad del juego a x3
|       |         |            |       |       ||