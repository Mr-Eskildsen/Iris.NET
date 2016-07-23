﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Iris.NET
{
    /// <summary>
    /// Base interface for a network node.
    /// </summary>
    public interface IIrisNode : IDisposable
    {
        /// <summary>
        /// Guid of this node.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Submits the content to the pubsub network.
        /// </summary>
        /// <param name="targetChannel">The channel targeted by the content. If it is "null" the content targets every client (broadcast).</param>
        /// <param name="content">The content to send.</param>
        /// <param name="propagateThroughHierarchy">Indicates if the content also targets all the clients who are subscribed to child channels compared to the target channel.</param>
        /// <returns>True if the operation succeeded.</returns>
        bool Send(string targetChannel, object content, bool propagateThroughHierarchy = false);

        /// <summary>
        /// Submits the content to every node in the pubsub network.
        /// </summary>
        /// <param name="content">The content to send.</param>
        /// <returns>True if the operation succeeded.</returns>
        bool SendToBroadcast(object content);

        /// <summary>
        /// Subscribes this node to a channel.
        /// </summary>
        /// <param name="channel">The channel to which subscribe.</param>
        /// <param name="contentHandler">A handler for the content received through this subscription.</param>
        /// <returns>An IDisposableSubscription which can be used to remove the content handler from the subscription, or null if the operation failed.</returns>
        IDisposableSubscription Subscribe(string channel, ContentHandler contentHandler);

        /// <summary>
        /// Subscribes the conten handler to the broadcast communication.
        /// </summary>
        /// <param name="contentHandler">A handler for the content received in broadcast.</param>
        /// <returns>An IDisposableSubscription which can be used to remove the content handler from the broadcast, or null if the operation failed.</returns>
        IDisposableSubscription SubscribeToBroadcast(ContentHandler contentHandler);

        /// <summary>
        /// Removes the content handler from the subscription.
        /// </summary>
        /// <param name="channel">The channel from which unsubscribe.</param>
        /// <param name="contentHandler">The content handler to be removed from this subscription.</param>
        /// <param name="keepUnderlyingSubscription">Indicates if the node should keep the underlying subscription to the channel in order to improve efficiency in future subscriptions to it.</param>
        /// <returns>True if the operation succeeded.</returns>
        bool Unsubscribe(string channel, ContentHandler contentHandler, bool keepUnderlyingSubscription = false);

        /// <summary>
        /// Removes the content handler from the broadcast communication.
        /// </summary>
        /// <param name="contentHandler">The content handler to be removed from the broadcast.</param>
        /// <returns>True if the operation succeeded.</returns>
        bool UnsubscribeFromBroadcast(ContentHandler contentHandler);

        /// <summary>
        /// Unsubscribes this node from a channel.
        /// All the content handlers subscribed to this channel will be lost.
        /// </summary>
        /// <param name="channel">The channel from which unsubscribe.</param>
        /// <returns>True if the operation succeeded.</returns>
        bool Unsubscribe(string channel);
    }

    /// <summary>
    /// Delegate definition for a content handler.
    /// </summary>
    /// <param name="content">The content received.</param>
    /// <param name="hook">Iris context hook for other information.</param>
    public delegate void ContentHandler(object content, IrisContextHook hook);
}
