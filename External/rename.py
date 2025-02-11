import os
import sys

def rename_mlod_items():
    # Get the current working directory where the script is executed
    current_dir = os.getcwd()
    print(f"Starting search in: {current_dir}")
    
    # Counter for renamed items
    renamed_count = 0
    
    # Walk through directory tree
    for root, dirs, files in os.walk(current_dir):
        # Check and rename directories
        for dir_name in dirs[:]:  # Create a copy of the list to modify during iteration
            if "_mlod" in dir_name:
                old_path = os.path.join(root, dir_name)
                new_name = dir_name.replace("_mlod", "")
                new_path = os.path.join(root, new_name)
                
                try:
                    os.rename(old_path, new_path)
                    print(f"Renamed directory: {old_path} -> {new_path}")
                    renamed_count += 1
                except Exception as e:
                    print(f"Error renaming directory {old_path}: {str(e)}")
        
        # Check and rename files
        for file_name in files:
            if "_mlod" in file_name:
                old_path = os.path.join(root, file_name)
                new_name = file_name.replace("_mlod", "")
                new_path = os.path.join(root, new_name)
                
                try:
                    os.rename(old_path, new_path)
                    print(f"Renamed file: {old_path} -> {new_path}")
                    renamed_count += 1
                except Exception as e:
                    print(f"Error renaming file {old_path}: {str(e)}")
    
    print(f"\nOperation complete. Total items renamed: {renamed_count}")

if __name__ == "__main__":
    # Print a warning and ask for confirmation
    print("WARNING: This script will rename all files and folders containing '_mlod' in their name.")
    print("The script will remove '_mlod' from the names of all matching items in the current directory and its subdirectories.")
    response = input("Do you want to continue? (y/n): ")
    
    if response.lower() == 'y':
        rename_mlod_items()
    else:
        print("Operation cancelled.")
