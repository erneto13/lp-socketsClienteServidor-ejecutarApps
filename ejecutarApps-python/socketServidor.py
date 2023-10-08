# -*- coding: utf-8 -*-
"""
Created on Sun Oct  8 01:42:21 2023

@author: Ernesto Amaral
"""

import socket
import subprocess

# Configuración del servidor
HOST = '0.0.0.0'  # Dirección IP del servidor
PORT = 8000  # Puerto del servidor

# Crear un socket del servidor
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Vincular el socket al host y puerto
server_socket.bind((HOST, PORT))

# Esperar a que un cliente se conecte
server_socket.listen()

print(f"Esperando conexiones en {HOST}:{PORT}...")

# Aceptar la conexión entrante
client_socket, client_address = server_socket.accept()
print(f"Conexión entrante desde {client_address}")

# Menú de aplicaciones remotas
menu = """
1. Ejecutar Notepad
2. Ejecutar Calculator
3. Ejecutar Word
4. Ejecutar Excel
5. Ejecutar PowerPoint
"""

client_socket.send(menu.encode())

# Recibir la selección del cliente
selection = client_socket.recv(1024).decode()

# Mapa de aplicaciones a ejecutar
app_map = {
    '1': 'notepad.exe',
    '2': 'calc.exe',
    '3': 'winword.exe',
    '4': 'excel.exe',
    '5': 'powerpnt.exe'
}

# Ejecutar la aplicación seleccionada
if selection in app_map:
    app = app_map[selection]
    subprocess.Popen(app)
    client_socket.send(f"Se ejecutó la aplicación: {app}".encode())
else:
    client_socket.send("Selección no válida".encode())

# Cerrar la conexión con el cliente
client_socket.close()

# Cerrar el socket del servidor
server_socket.close()
