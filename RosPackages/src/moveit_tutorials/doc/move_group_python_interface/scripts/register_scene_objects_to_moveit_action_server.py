#! /usr/bin/env python

import rospy
import actionlib
import moveit_tutorials.msg
import moveit_commander
import geometry_msgs.msg

class RegisterSceneObjectsToMoveitActionServer(object):

    def __init__(self, name, scene):
    # create messages that are used to publish feedback/result
        self._feedback = moveit_tutorials.msg.RegisterSceneObjectToMoveitFeedback()
        self._result   = moveit_tutorials.msg.RegisterSceneObjectToMoveitResult()

        self._action_name = name
        self._scene = scene
        self._as = actionlib.SimpleActionServer(self._action_name, moveit_tutorials.msg.RegisterSceneObjectToMoveitAction, execute_cb=self.execute_cb, auto_start = False)
        self._as.start()
    

    def add_mesh(self, meshName, meshByte):
        rospy.loginfo("hit add_mesh")

        box_pose_stamped = geometry_msgs.msg.PoseStamped()
        box_pose_stamped.header.frame_id = "iiwa_link_0"
        box_pose_stamped.pose = geometry_msgs.msg.Pose()
        mesh_name = meshName

        rospy.loginfo("hit before write stl")

        fileName = "/home/berkayalpcakal/Desktop/mesh.stl"
        with open(fileName, "wb") as stlFile:
            stlFile.write(meshByte)
            stlFile.close()

        rospy.loginfo("hit after write stl")

        self._scene.add_mesh(mesh_name, box_pose_stamped, fileName) 

        return
       

    def add_box(self, boxName, boxPose, boxSize):
        box_pose_stamped = geometry_msgs.msg.PoseStamped()
        box_pose_stamped.header.frame_id = "iiwa_link_0"
        box_pose_stamped.pose = boxPose
        box_name = boxName

        self._scene.add_box(box_name, box_pose_stamped, size=(boxSize[0], boxSize[1], boxSize[2]))
        return

    def execute_cb(self, goal):
        rospy.loginfo('callback hit')
        success = True

        rospy.loginfo("Size of mesh: " + str(len(goal.mesh)))
        if len(goal.mesh) > 0: 
            #rospy.loginfo("mesh[0]: " + str(goal.mesh[0]))
            self.add_mesh("HoloMesh", goal.mesh)
        else:
            self._result.log = 'invalid mesh size, wont be added'

        if len(goal.names) > 0:
            self._feedback.log = str(len(goal.names)) + ' number of objects are being added.. \n'
            rospy.loginfo(self._feedback.log)

            for i in range(len(goal.names)):
                if self._as.is_preempt_requested():
                    rospy.loginfo('%s: Preempted' % self._action_name)
                    self._as.set_preempted()
                    success = False
                    break
                try:
                    pose = geometry_msgs.msg.Pose()
                    pose.position.x = goal.positions_x[i]
                    pose.position.y = goal.positions_y[i]
                    pose.position.z = goal.positions_z[i]
                    pose.orientation.x = goal.orientations_x[i]
                    pose.orientation.y = goal.orientations_y[i]
                    pose.orientation.z = goal.orientations_z[i]
                    pose.orientation.w = goal.orientations_w[i]

                    size = [goal.sizes_x[i], goal.sizes_y[i], goal.sizes_z[i]]

                    rospy.loginfo('adding object: ' + str(i))
                    self.add_box(goal.names[i], pose, size)
                    
                    rospy.loginfo('added object: ' + str(i))
                    self._feedback.log = goal.names[i] + ' is added..' 
                    self._as.publish_feedback(self._feedback)
                except:
                    self._feedback.log = goal.names[i] + ' could not be added..'
                    self._as.publish_feedback(self._feedback)

            if success:
                self._result.log = 'objects are added to the scene'
                rospy.loginfo('%s: Succeeded' % self._action_name)
                self._as.set_succeeded(self._result)

        else:
            self._result.log = 'invalid number of objects'
        
