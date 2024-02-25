import React, { useState, useEffect } from 'react';
import firebase from 'firebase/compat/app';
import 'firebase/compat/firestore';

const AdminPanel = ({ currentUser, db }) => {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const fetchUsers = async () => {
      const usersSnapshot = await db.collection('users').get();
      const fetchedUsers = usersSnapshot.docs.map(doc => ({
        id: doc.id,
        ...doc.data()
      }));
      setUsers(fetchedUsers);
    };
    
    fetchUsers();
  }, []);

  const handleRoleChange = async (userId, newRole) => {
    if (currentUser.role === 'admin') {
      await db.collection('users').doc(userId).update({ role: newRole });
      setUsers(prevUsers => prevUsers.map(user =>
        user.id === userId ? { ...user, role: newRole } : user
      ));
    }
  };

  const handleDeleteUser = async (userId) => {
    if (currentUser.role !== 'admin') {
      return; 
    }

    if (userId === currentUser.id) {
      alert("You cannot delete yourself!");
      return;
    }

    if (window.confirm(`Are you sure you want to delete user ${userId}? This action cannot be undone.`)) {
      try {
        await db.collection('users').doc(userId).delete();

        setUsers(prevUsers => prevUsers.filter(user => user.id !== userId));
      } catch (error) {
        alert(`Error deleting user: ${error.message}`);
      }
    }
  };

  return (
    <div>
      <h2>Admin Panel</h2>
      <p>Current User: {currentUser.email}</p>
      <ul>
        {users.map(user => (
          <li key={user.id}>
            {user.email} - {user.role}
            {currentUser.role === 'admin' && user.role !== 'admin' && (
              <>
                <select value={user.role} onChange={(e) => handleRoleChange(user.id, e.target.value)}>
                  <option value="user">User</option>
                  <option value="it_staff">IT Staff</option>
                  <option value="it_manager">IT Manager</option>
                  <option value="general_manager">General Manager</option>
                  <option value="bankers">Bankers</option>
                  <option value="admin">Admin</option>
                  {/* Add other roles here */}
                </select>
                <button onClick={() => handleDeleteUser(user.id)}>Delete</button>
              </>
            )}
            {currentUser.role === 'admin' && currentUser.id === user.id && (
              <span>You cannot change your own role</span>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AdminPanel;
