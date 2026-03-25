# Delete Rows

## About

This script deletes one or more rows from a table in a DataMiner element. It is designed to be used with GQI (Generic Query Interface) or as a standalone automation script.

## Input Parameters

The script requires the following input parameters:

| Name | Description | Format |
| ---- | ----------- | ------ |
| Element Identifier | The element containing the table | - Element name (e.g., `MyElement`)<br>- DataMinerID/ElementID (e.g., `123/456`) |
| Table Identifier | The parameter ID of the table | A valid integer representing the parameter ID of the table in the element's protocol (e.g., `1000`) |
| Primary Key(s) | The primary key(s) of the row(s) to delete | If you provide multiple row keys, separate them by commas (`,`) or semicolons (`;`). For example, `key1,key2,key3`.<br>Leading and trailing whitespace will be automatically trimmed. |

These parameters can also be filled in via a GQI query, allowing for dynamic input based on query results.

## Error Handling

The script can return the following error messages:

- The element identifier is empty or invalid
- The table ID is empty, whitespace, or not a valid integer
- No valid row keys are provided
- The element cannot be found in the DataMiner System
- The element is not started or has not completed its startup routine
- The specified table does not exist in the element's protocol
- The user does not have permission to delete rows from the specified table
- The script encounters an error while attempting to delete the specified rows
- An unexpected exception occurs during execution
