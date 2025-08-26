# Readme

This project uses extremely strict naming conventions for model name suffix.

| Suffix | Meaning              | Description                                                                                                                                          | 
|--------|----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Dto`  | Data Transfer Object | This is the model serialised and transmitted over the network to the client.                                                                         |
| `Jm`   | JSON Model           | Similar to `Dto`, this model is serialised and transmitted over the network. <br> However, this model is also serialised and stored in the database. | 
| `Dbm`  | Database Model       | This is the model stored in the database and used by the ORM.                                                                                        | 