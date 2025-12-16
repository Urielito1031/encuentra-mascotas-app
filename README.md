# Encuentra Mascotas – Backend API

Este repositorio contiene la **API Backend** de *Encuentra Mascotas*, una plataforma orientada a la búsqueda de mascotas perdidas mediante **búsqueda visual por similitud de imágenes**.

La funcionalidad central del sistema es el **Visual Search**:  
permite cargar una imagen de una mascota y recuperar publicaciones visualmente similares a partir de la comparación de **embeddings vectoriales**, evitando depender únicamente de filtros tradicionales como raza, color o tamaño.

El proyecto está diseñado siguiendo **Clean Architecture**, **Domain-Driven Design (DDD)** y **CQRS**, priorizando un modelo de dominio consistente, mantenible y preparado para escalar.

> Nota: El frontend será incorporado en una etapa posterior del proyecto.

---

## 🛠 Tecnologías Principales

- **.NET 9**
- **Clean Architecture** (Domain, Application, Infrastructure, API)
- **Domain-Driven Design (DDD)**
- **CQRS**
- **PostgreSQL 16 + pgvector**
- **Entity Framework Core (Code First)**
- **Docker & Docker Compose**
- **ONNX Runtime (CLIP – Vision Model)** para generación local de embeddings

---

## 🧠 Arquitectura

- El **Dominio** encapsula las reglas de negocio y no depende de infraestructura.
- La **Capa de Aplicación** implementa los casos de uso (UseCases) y orquesta el flujo.
- La **Infraestructura** contiene persistencia, configuraciones de EF Core y dependencias técnicas.
- La **API** expone endpoints REST y actúa como punto de entrada al sistema.

Las búsquedas vectoriales se realizan sobre PostgreSQL utilizando la extensión **pgvector**, permitiendo consultas eficientes por similitud coseno.

---

## 🚀 Ejecución del Proyecto (Entorno Local)

### 1️⃣ Levantar la Base de Datos

Asegúrate de tener Docker en ejecución. Desde la carpeta del proyecto:

```bash
docker-compose up -d
