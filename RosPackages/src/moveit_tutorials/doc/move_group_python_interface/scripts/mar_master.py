#!/usr/bin/env python

# Software License Agreement (BSD License)
#
# Copyright (c) 2013, SRI International
# All rights reserved.
#
# Redistribution and use in source and binary forms, with or without
# modification, are permitted provided that the following conditions
# are met:
#
#  * Redistributions of source code must retain the above copyright
#    notice, this list of conditions and the following disclaimer.
#  * Redistributions in binary form must reproduce the above
#    copyright notice, this list of conditions and the following
#    disclaimer in the documentation and/or other materials provided
#    with the distribution.
#  * Neither the name of SRI International nor the names of its
#    contributors may be used to endorse or promote products derived

import  sys
import  copy
import  rospy
import  moveit_commander
import  moveit_msgs.msg
import  geometry_msgs.msg
from    move_group_python_interface_tutorial            import MoveGroupPythonIntefaceTutorial
from    register_scene_objects_to_moveit_action_server  import RegisterSceneObjectsToMoveitActionServer
from    plan_path_action_server                         import PlanPathActionServer
from    execute_path_action_server                      import ExecutePathActionServer
from    publish_trajectory_positions                    import PublishTrajectoryPositions
from    math                                            import pi
from    std_msgs.msg                                    import String
from    moveit_commander.conversions                    import pose_to_list
from    moveit_msgs.msg                                 import PlanningScene, CollisionObject, AttachedCollisionObject
from    geometry_msgs.msg                               import PoseStamped, Pose, Point


class MarMaster(object):

  def __init__(self):
    super(MarMaster, self).__init__()
    ## Initialize move_group path planner
    move_group_interface = MoveGroupPythonIntefaceTutorial()
    move_group_interface.move_group.set_max_velocity_scaling_factor(0.3)
    move_group_interface.scene._pub_co = rospy.Publisher('/iiwa/collision_object', CollisionObject, queue_size=100)

    ## Initialize RegisterSceneObjectsToMoveit ROS Action Server
    register_scene_object_action_server = RegisterSceneObjectsToMoveitActionServer('register_scene_objects_to_moveit', move_group_interface.scene)
    
    ## Initialize PlanPath ROS Action Server
    plan_path_action_server = PlanPathActionServer('plan_path', move_group_interface.move_group)

    ## Initialize ExecutePath ROS Action Server
    execute_path_action_server = ExecutePathActionServer('execute_path', move_group_interface.move_group, plan_path_action_server)

    ## Initialize TrajectoryPublisher
    trajectory_publisher = PublishTrajectoryPositions()

if __name__ == '__main__':
    mar_master_node = MarMaster()
    rospy.spin()
    
